﻿using System;
using System.Collections.Generic;
using System.Linq;
using FeedParserPCL.Item;

namespace FeedParserPCL {
    using System.Xml.Linq;

    /// <summary>
    /// A simple RSS, RDF and ATOM feed parser.
    /// </summary>
    public static class FeedParser {
        public static IEnumerable<IItem> Parse(string content, FeedType feedType) {
            var root = XDocument.Parse (content).Root;
            if (root == null)
                throw new FormatException (nameof (root));
            switch (feedType) {
                case FeedType.Rss:
                    return ParseRss (root);
                case FeedType.Rdf:
                    return ParseRdf (root);
                case FeedType.Atom:
                    return ParseAtom (root);
                default:
                    throw new NotSupportedException (feedType + " is not supported");
            }
        }

        /// <summary>
        /// Parses an Atom feed and returns a list of items.
        /// </summary>
        /// <returns>A list of items</returns>
        private static IEnumerable<IItem> ParseAtom(XContainer root) {
            return root.Elements ()
                .Where (i => i.Name.LocalName.Equals ("entry"))
                .Select (item => new AtomItem ().Parse (item));
        }

        /// <summary>
        /// Parses an RSS feed and returns a list of items.
        /// </summary>
        /// <returns>A list of items</returns>
        private static IEnumerable<IItem> ParseRss(XContainer root) {
            return root.Descendants ().First (i => i.Name.LocalName == "channel").Elements ()
                .Where (i => i.Name.LocalName == "item")
                .Select (item => new RssItem ().Parse (item));
        }

        /// <summary>
        /// Parses an RDF feed and returns a list of items
        /// </summary>
        /// <returns>A list of items</returns>
        private static IEnumerable<IItem> ParseRdf(XContainer root) {
            return root.Descendants ()
                .Where (i => i.Name.LocalName == "item")
                .Select (item => new RdfItem ().Parse (item));
        }
    }
}