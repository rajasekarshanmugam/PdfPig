﻿namespace UglyToad.PdfPig.PdfFonts.Cmap
{
    using Core;
    using Parser;
    using System;
    using System.Collections.Generic;

    internal static class CMapCache
    {
        private static readonly Dictionary<string, CMap> Cache = new Dictionary<string, CMap>(StringComparer.OrdinalIgnoreCase);
        private static readonly object Lock = new object();

        private static readonly CMapParser CMapParser = new CMapParser();

        public static bool TryGet(string name, out CMap result)
        {
            result = null;

            lock (Lock)
            {
                if (Cache.TryGetValue(name, out result))
                {
                    return true;
                }

                if (CMapParser.TryParseExternal(name, out result))
                {

                    Cache[name] = result;

                    return true;
                }

                return false;
            }
        }

        public static CMap Parse(IInputBytes bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var result = CMapParser.Parse(bytes);

            return result;
        }
    }
}
