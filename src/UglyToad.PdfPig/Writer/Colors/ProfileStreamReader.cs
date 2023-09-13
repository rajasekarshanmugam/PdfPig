﻿namespace UglyToad.PdfPig.Writer.Colors
{
    using PdfFonts.Parser;
    using System;
    using System.IO;
    using System.Linq;

    internal static class ProfileStreamReader
    {
        public static byte[] GetSRgb2014()
        {
            var resources = typeof(ProfileStreamReader).Assembly.GetManifestResourceNames();

            var resource = resources.FirstOrDefault(x =>
                x.EndsWith("sRGB2014.icc", StringComparison.InvariantCultureIgnoreCase));

            if (resource == null)
            {
                throw new InvalidOperationException("Could not find the sRGB ICC color profile stream.");
            }

            byte[] bytes;
            using (var stream = typeof(CMapParser).Assembly.GetManifestResourceStream(resource))
            using (var memoryStream = new MemoryStream())
            {
                stream?.CopyTo(memoryStream);

                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
    }
}
