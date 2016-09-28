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
        public static IEnumerable<IItem> Parse(string content, FeedType feedType) {
            var root = XDocument.Parse (content).Root;
            if (root == null)
                throw new FormatException (nameof (root));
            switch (feedType) {
                case FeedType.Atom:
                    return new Atom (root);
                case FeedType.Rss:
                    return new Rss (root);
                case FeedType.Rdf:
                    return new Rdf (root);
                default:
                    throw new NotSupportedException (feedType + " is not supported");
            }
        }
    }
}