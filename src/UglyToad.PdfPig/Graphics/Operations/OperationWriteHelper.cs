﻿namespace UglyToad.PdfPig.Graphics.Operations
{
    using PdfPig.Core;
    using System;
    using System.Globalization;
    using System.IO;

    internal static class OperationWriteHelper
    {
        private static readonly byte WhiteSpace = OtherEncodings.StringAsLatin1Bytes(" ")[0];
        private static readonly byte NewLine = OtherEncodings.StringAsLatin1Bytes("\n")[0];

        public static void WriteText(this Stream stream, string text, bool appendWhitespace = false)
        {
            var bytes = OtherEncodings.StringAsLatin1Bytes(text);
            stream.Write(bytes, 0, bytes.Length);
            if (appendWhitespace)
            {
                stream.WriteWhiteSpace();
            }
        }

        public static void WriteHex(this Stream stream, byte[] bytes)
        {
            var text = BitConverter.ToString(bytes).Replace("-", string.Empty);
            stream.WriteText($"<{text}>");
        }

        public static void WriteWhiteSpace(this Stream stream)
        {
            stream.WriteByte(WhiteSpace);
        }

        public static void WriteNewLine(this Stream stream)
        {
            stream.WriteByte(NewLine);
        }

        public static void WriteDecimal(this Stream stream, decimal value)
        {
            stream.WriteText(value.ToString("G", CultureInfo.InvariantCulture));
        }

        public static void WriteNumberText(this Stream stream, decimal number, string text)
        {
            stream.WriteDecimal(number);
            stream.WriteWhiteSpace();
            stream.WriteText(text);
            stream.WriteNewLine();
        }
    }
}
