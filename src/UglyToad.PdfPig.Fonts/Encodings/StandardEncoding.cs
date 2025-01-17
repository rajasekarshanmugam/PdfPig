﻿namespace UglyToad.PdfPig.Fonts.Encodings
{
    /// <summary>
    /// The standard PDF encoding.
    /// </summary>
    public class StandardEncoding : Encoding
    {
        private static readonly (int, string)[] EncodingTable =
        {
            (0101, "A"),
            (0341, "AE"),
            (0102, "B"),
            (0103, "C"),
            (0104, "D"),
            (0105, "E"),
            (0106, "F"),
            (0107, "G"),
            (0110, "H"),
            (0111, "I"),
            (0112, "J"),
            (0113, "K"),
            (0114, "L"),
            (0350, "Lslash"),
            (0115, "M"),
            (0116, "N"),
            (0117, "O"),
            (0352, "OE"),
            (0351, "Oslash"),
            (0120, "P"),
            (0121, "Q"),
            (0122, "R"),
            (0123, "S"),
            (0124, "T"),
            (0125, "U"),
            (0126, "V"),
            (0127, "W"),
            (0130, "X"),
            (0131, "Y"),
            (0132, "Z"),
            (0141, "a"),
            (0302, "acute"),
            (0361, "ae"),
            (0046, "ampersand"),
            (0136, "asciicircum"),
            (0176, "asciitilde"),
            (0052, "asterisk"),
            (0100, "at"),
            (0142, "b"),
            (0134, "backslash"),
            (0174, "bar"),
            (0173, "braceleft"),
            (0175, "braceright"),
            (0133, "bracketleft"),
            (0135, "bracketright"),
            (0306, "breve"),
            (0267, "bullet"),
            (0143, "c"),
            (0317, "caron"),
            (0313, "cedilla"),
            (0242, "cent"),
            (0303, "circumflex"),
            (0072, "colon"),
            (0054, "comma"),
            (0250, "currency"),
            (0144, "d"),
            (0262, "dagger"),
            (0263, "daggerdbl"),
            (0310, "dieresis"),
            (0044, "dollar"),
            (0307, "dotaccent"),
            (0365, "dotlessi"),
            (0145, "e"),
            (0070, "eight"),
            (0274, "ellipsis"),
            (0320, "emdash"),
            (0261, "endash"),
            (0075, "equal"),
            (0041, "exclam"),
            (0241, "exclamdown"),
            (0146, "f"),
            (0256, "fi"),
            (0065, "five"),
            (0257, "fl"),
            (0246, "florin"),
            (0064, "four"),
            (0244, "fraction"),
            (0147, "g"),
            (0373, "germandbls"),
            (0301, "grave"),
            (0076, "greater"),
            (0253, "guillemotleft"),
            (0273, "guillemotright"),
            (0254, "guilsinglleft"),
            (0255, "guilsinglright"),
            (0150, "h"),
            (0315, "hungarumlaut"),
            (0055, "hyphen"),
            (0151, "i"),
            (0152, "j"),
            (0153, "k"),
            (0154, "l"),
            (0074, "less"),
            (0370, "lslash"),
            (0155, "m"),
            (0305, "macron"),
            (0156, "n"),
            (0071, "nine"),
            (0043, "numbersign"),
            (0157, "o"),
            (0372, "oe"),
            (0316, "ogonek"),
            (0061, "one"),
            (0343, "ordfeminine"),
            (0353, "ordmasculine"),
            (0371, "oslash"),
            (0160, "p"),
            (0266, "paragraph"),
            (0050, "parenleft"),
            (0051, "parenright"),
            (0045, "percent"),
            (0056, "period"),
            (0264, "periodcentered"),
            (0275, "perthousand"),
            (0053, "plus"),
            (0161, "q"),
            (0077, "question"),
            (0277, "questiondown"),
            (0042, "quotedbl"),
            (0271, "quotedblbase"),
            (0252, "quotedblleft"),
            (0272, "quotedblright"),
            (0140, "quoteleft"),
            (0047, "quoteright"),
            (0270, "quotesinglbase"),
            (0251, "quotesingle"),
            (0162, "r"),
            (0312, "ring"),
            (0163, "s"),
            (0247, "section"),
            (0073, "semicolon"),
            (0067, "seven"),
            (0066, "six"),
            (0057, "slash"),
            (0040, "space"),
            (0243, "sterling"),
            (0164, "t"),
            (0063, "three"),
            (0304, "tilde"),
            (0062, "two"),
            (0165, "u"),
            (0137, "underscore"),
            (0166, "v"),
            (0167, "w"),
            (0170, "x"),
            (0171, "y"),
            (0245, "yen"),
            (0172, "z"),
            (0060, "zero")
        };

        /// <summary>
        /// The single instance of the standard encoding.
        /// </summary>
        public static StandardEncoding Instance { get; } = new StandardEncoding();

        /// <inheritdoc />
        public override string EncodingName => "StandardEncoding";

        private StandardEncoding()
        {
            foreach ((var codeToBeConverted, var name) in EncodingTable)
            {
                // In source code an int literal with a leading zero ('0')
                // in other languages ('C' and 'Java') would be interpreted
                // as octal (base 8) and converted but C# does not support and
                // so arrives here as a different value parsed as base10.
                // Convert 'codeToBeConverted' to intended value as if it was an octal literal before using.
                // For example 040 converts to string "40" then convert string to int again but using base 8 (octal) so result is 32 (base 10).
                var code = System.Convert.ToInt32($"{codeToBeConverted}", 8);  // alternative is OctalHelpers.FromOctalInt()
                Add(code, name);
            }
        }
    }
}