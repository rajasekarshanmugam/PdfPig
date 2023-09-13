﻿namespace UglyToad.PdfPig.Graphics.Operations.TextObjects
{
    using PdfPig.Core;
    using System.IO;

    /// <inheritdoc />
    /// <summary>
    /// End a text object, discarding the text matrix.
    /// </summary>
    public class EndText : IGraphicsStateOperation
    {
        /// <summary>
        /// The symbol for this operation in a stream.
        /// </summary>
        public const string Symbol = "ET";

        /// <summary>
        /// The instance of the <see cref="EndText"/> operation.
        /// </summary>
        public static readonly EndText Value = new EndText();

        /// <inheritdoc />
        public string Operator => Symbol;

        private EndText()
        {
        }

        /// <inheritdoc />
        public void Run(IOperationContext operationContext)
        {
            operationContext.TextMatrices.TextMatrix = TransformationMatrix.Identity;
            operationContext.TextMatrices.TextLineMatrix = TransformationMatrix.Identity;
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