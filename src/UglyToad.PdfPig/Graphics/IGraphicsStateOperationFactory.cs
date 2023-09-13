namespace UglyToad.PdfPig.Graphics
{
    using Operations;
    using System.Collections.Generic;
    using Tokens;
    using Util.JetBrains.Annotations;

    internal interface IGraphicsStateOperationFactory
    {
        [CanBeNull]
        IGraphicsStateOperation Create(OperatorToken op, IReadOnlyList<IToken> operands);
    }
}