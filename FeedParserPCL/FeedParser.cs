using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FeedParserPCL.Feed;
using FeedParserPCL.Feed.RdfBased;

namespace FeedParserPCL {
    /// <summary>
    /// A simple RSS, RDF and ATOM feed parser.
    /// </summary>
    public static class FeedParser {
        /// <summary>
        /// Parse a feed base on its own type.
        /// </summary>
        /// <param name="feed">the feed to be parsed</param>
        /// <param name="type">the feed's own type</param>
        /// <returns>The content as a enumerable of item</returns>
        /// <example>
        /// var feed = FeedParser.Parse (File.ReadAllText ("./feed.rss), FeedType.Rss);
        /// foreach (var item in feed)
        ///     Console.WriteLine ($"{item.PublishDate} {item.Title}");
        /// </example>
        public static IEnumerable<IItem> Parse(string feed, FeedType type) {
            var root = XDocument.Parse (feed).Root;
            if (root == null)
                throw new FormatException (nameof (root));
            switch (type) {
                case FeedType.Atom: return new Atom (root);
                case FeedType.Rss: return new Rss (root);
                case FeedType.Rdf: return new Rdf (root);
                default: throw new NotSupportedException (type + " is not supported");
            }
        }
    }
}