namespace UglyToad.PdfPig.PdfFonts.CidFonts
{
    using Core;
    using System;

    /// <summary>
    /// Represents either an Adobe Type 1 or TrueType font program for a CIDFont.
    /// </summary>
    internal interface ICidFontProgram
    {
        FontDetails Details { get; }

        bool TryGetBoundingBox(int characterIdentifier, out PdfRectangle boundingBox);

        bool TryGetBoundingBox(int characterIdentifier, Func<int, int?> characterCodeToGlyphId, out PdfRectangle boundingBox);

        bool TryGetBoundingAdvancedWidth(int characterIdentifier, Func<int, int?> characterCodeToGlyphId, out double width);

        bool TryGetBoundingAdvancedWidth(int characterIdentifier, out double width);

        int GetFontMatrixMultiplier();
    }
}
