﻿namespace UglyToad.PdfPig.Annotations
{
    using Core;
    using Parser.Parts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tokenization.Scanner;
    using Tokens;
    using Util;

    internal class AnnotationProvider
    {
        private readonly IPdfTokenScanner tokenScanner;
        private readonly DictionaryToken pageDictionary;

        public AnnotationProvider(IPdfTokenScanner tokenScanner, DictionaryToken pageDictionary)
        {
            this.tokenScanner = tokenScanner ?? throw new ArgumentNullException(nameof(tokenScanner));
            this.pageDictionary = pageDictionary ?? throw new ArgumentNullException(nameof(pageDictionary));
        }

        public IEnumerable<Annotation> GetAnnotations()
        {
            if (!pageDictionary.TryGet(NameToken.Annots, tokenScanner, out ArrayToken annotationsArray))
            {
                yield break;
            }

            foreach (var token in annotationsArray.Data)
            {
                if (!DirectObjectFinder.TryGet(token, tokenScanner, out DictionaryToken annotationDictionary))
                {
                    continue;
                }

                var type = annotationDictionary.Get<NameToken>(NameToken.Subtype, tokenScanner);

                var annotationType = type.ToAnnotationType();
                var rectangle = annotationDictionary.Get<ArrayToken>(NameToken.Rect, tokenScanner).ToRectangle(tokenScanner);

                var contents = GetNamedString(NameToken.Contents, annotationDictionary);
                var name = GetNamedString(NameToken.Nm, annotationDictionary);
                var modifiedDate = GetNamedString(NameToken.M, annotationDictionary);

                var flags = (AnnotationFlags)0;
                if (annotationDictionary.TryGet(NameToken.F, out var flagsToken) && DirectObjectFinder.TryGet(flagsToken, tokenScanner, out NumericToken flagsNumericToken))
                {
                    flags = (AnnotationFlags)flagsNumericToken.Int;
                }

                var border = AnnotationBorder.Default;
                if (annotationDictionary.TryGet(NameToken.Border, out var borderToken) && DirectObjectFinder.TryGet(borderToken, tokenScanner, out ArrayToken borderArray)
                    && borderArray.Length >= 3)
                {
                    var horizontal = borderArray.GetNumeric(0).Data;
                    var vertical = borderArray.GetNumeric(1).Data;
                    var width = borderArray.GetNumeric(2).Data;
                    var dashes = default(IReadOnlyList<decimal>);

                    if (borderArray.Length == 4 && borderArray.Data[4] is ArrayToken dashArray)
                    {
                        dashes = dashArray.Data.OfType<NumericToken>().Select(x => x.Data).ToList();
                    }

                    border = new AnnotationBorder(horizontal, vertical, width, dashes);
                }

                var quadPointRectangles = new List<QuadPointsQuadrilateral>();
                if (annotationDictionary.TryGet(NameToken.Quadpoints, tokenScanner, out ArrayToken quadPointsArray))
                {
                    var values = new List<decimal>();
                    for (var i = 0; i < quadPointsArray.Length; i++)
                    {
                        if (!(quadPointsArray[i] is NumericToken value))
                        {
                            continue;
                        }

                        values.Add(value.Data);

                        if (values.Count == 8)
                        {
                            quadPointRectangles.Add(new QuadPointsQuadrilateral(new[]
                            {
                                new PdfPoint(values[0], values[1]),
                                new PdfPoint(values[2], values[3]),
                                new PdfPoint(values[4], values[5]),
                                new PdfPoint(values[6], values[7])
                            }));

                            values.Clear();
                        }
                    }
                }

                yield return new Annotation(annotationDictionary, annotationType, rectangle, contents, name, modifiedDate, flags, border,
                    quadPointRectangles);
            }
        }

        private string GetNamedString(NameToken name, DictionaryToken dictionary)
        {
            string content = null;
            if (dictionary.TryGet(name, out var contentToken))
            {
                if (contentToken is StringToken contentString)
                {
                    content = contentString.Data;
                }
                else if (contentToken is HexToken contentHex)
                {
                    content = contentHex.Data;
                }
                else if (DirectObjectFinder.TryGet(contentToken, tokenScanner, out StringToken indirectContentString))
                {
                    content = indirectContentString.Data;
                }
            }

            return content;
        }
    }
}