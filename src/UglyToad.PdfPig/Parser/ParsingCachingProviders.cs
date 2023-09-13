namespace UglyToad.PdfPig.Parser
{
    using Content;
    using System;

    /// <summary>
    /// For objects which provide document scoped caching.
    /// </summary>
    internal class ParsingCachingProviders
    {
        public IResourceStore ResourceContainer { get; }

        public ParsingCachingProviders(IResourceStore resourceContainer)
        {
            ResourceContainer = resourceContainer ?? throw new ArgumentNullException(nameof(resourceContainer));
        }
    }
}
