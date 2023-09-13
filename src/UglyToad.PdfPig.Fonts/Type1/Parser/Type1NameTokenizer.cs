namespace UglyToad.PdfPig.Fonts.Type1.Parser
{
    using Core;
    using System.Text;
    using Tokenization;
    using Tokens;

    /// <inheritdoc />
    public class Type1NameTokenizer : ITokenizer
    {
        /// <inheritdoc />
        public bool ReadsNextByte { get; } = true;

        /// <inheritdoc />
        public bool TryTokenize(byte currentByte, IInputBytes inputBytes, out IToken token)
        {
            token = null;

            if (currentByte != '/')
            {
                return false;
            }

            var builder = new StringBuilder();
            while (inputBytes.MoveNext())
            {
                if (ReadHelper.IsWhitespace(inputBytes.CurrentByte)
                    || inputBytes.CurrentByte == '{'
                    || inputBytes.CurrentByte == '<'
                    || inputBytes.CurrentByte == '/'
                    || inputBytes.CurrentByte == '['
                    || inputBytes.CurrentByte == '(')
                {
                    break;
                }

                builder.Append((char)inputBytes.CurrentByte);
            }

            token = NameToken.Create(builder.ToString());

            return true;
        }
    }
}
