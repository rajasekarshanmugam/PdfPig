﻿namespace UglyToad.PdfPig.Graphics.Operations.PathPainting
{
    using System.IO;

    /// <inheritdoc />
    /// <summary>
    /// Close, fill, and then stroke the path, using the nonzero winding number rule to determine the region to fill. 
    /// </summary>
    public class CloseFillPathNonZeroWindingAndStroke : IGraphicsStateOperation
    {
        /// <summary>
        /// The symbol for this operation in a stream.
        /// </summary>
        public const string Symbol = "b";

        /// <summary>
        /// The instance of the <see cref="CloseFillPathNonZeroWindingAndStroke"/> operation.
        /// </summary>
        public static readonly CloseFillPathNonZeroWindingAndStroke Value = new CloseFillPathNonZeroWindingAndStroke();

        /// <inheritdoc />
        public string Operator => Symbol;

        private CloseFillPathNonZeroWindingAndStroke()
        {
        }

        /// <inheritdoc />
        public void Run(IOperationContext operationContext)
        {
            operationContext.FillStrokePath(PdfPig.Core.FillingRule.NonZeroWinding, true);
        }

        /// <inheritdoc />
        public void Write(Stream stream)
        {
            stream.WriteText(Symbol);
            stream.WriteNewLine();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Symbol;
        }
    }
}