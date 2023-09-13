namespace UglyToad.PdfPig.Parser
{
    using Core;
    using Graphics.Operations;
    using Logging;
    using System.Collections.Generic;

    internal interface IPageContentParser
    {
        IReadOnlyList<IGraphicsStateOperation> Parse(int pageNumber, IInputBytes inputBytes,
            ILog log);
    }
}