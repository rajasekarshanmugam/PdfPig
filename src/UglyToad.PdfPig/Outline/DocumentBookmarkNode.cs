﻿namespace UglyToad.PdfPig.Outline
{
    using Destinations;
    using System;
    using System.Collections.Generic;

    /// <inheritdoc />
    /// <summary>
    /// A node in the <see cref="Bookmarks" /> of a PDF document which corresponds
    /// to a location in the current document.
    /// </summary>
    public class DocumentBookmarkNode : BookmarkNode
    {
        /// <summary>
        /// The page number where the bookmark is located.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// The destination of the bookmark in the current document.
        /// </summary>
        public ExplicitDestination Destination { get; }

        /// <inheritdoc />
        /// <summary>
        /// Create a new <see cref="DocumentBookmarkNode"/>.
        /// </summary>
        public DocumentBookmarkNode(string title, int level, ExplicitDestination destination, IReadOnlyList<BookmarkNode> children)
            : base(title, level, children)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            PageNumber = destination.PageNumber;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"page #{PageNumber}, {Level}, {Title}";
        }
    }
}