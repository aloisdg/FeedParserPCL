namespace FeedParserPCL
{
        /// <summary>
        /// Represents the XML format of a feed.
	/// <see cref="https://en.wikipedia.org/wiki/Category:Web_syndication_formats"/>
        /// </summary>
        public enum FeedType
        {
                /// <summary>
                /// Really Simple Syndication format.
		/// <see cref="https://en.wikipedia.org/wiki/RSS"/>
                /// </summary>
                Rss,

                /// <summary>
                /// RDF site summary format.
		/// <see cref="https://en.wikipedia.org/wiki/Resource_Description_Framework"/>
                /// </summary>
                Rdf,

                /// <summary>
                /// Atom Syndication format.
		/// <see cref="https://en.wikipedia.org/wiki/Atom_%28standard%29"/>
                /// </summary>
                Atom
        }
}