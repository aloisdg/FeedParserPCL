using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FeedParserPCL.Item;
using FeedParserPCL.Item.RdfBased;

namespace FeedParserPCL {
    using System.Xml.Linq;

    /// <summary>
    /// A simple RSS, RDF and ATOM feed parser.
    /// </summary>
    public static class FeedParser {
        private static Func<XElement, bool> IsNameIsItem => item => item.IsContainerName ("item");

        private static IEnumerable<IItem> Filter(IEnumerable<XElement> x,
            Func<XElement, bool> y,
            Func<XElement, IItem> z
            ) => x.Where (y).Select (z);

        public static IEnumerable<IItem> Parse(string content, FeedType feedType) {
            var root = XDocument.Parse (content).Root;
            if (root == null)
                throw new FormatException (nameof (root));
            switch (feedType) {
                case FeedType.Rss:
                    return root.Descendants ().First (item => item.IsContainerName ("channel")).Elements ()
                        .Where (IsNameIsItem)
                        .Select (item => new RssItem (item));
                case FeedType.Rdf:
                    return root.Descendants ()
                        .Where (IsNameIsItem)
                        .Select (item => new RdfItem (item));
                case FeedType.Atom:
                    return Atom.Create (root);
                default:
                    throw new NotSupportedException (feedType + " is not supported");
            }
        }

        public static class Atom {
            public static IEnumerable<IItem> Create(XElement element) {
                return element.Elements ()
                        .Where (item => item.IsContainerName ("entry"))
                        .Select (item => new AtomItem (item));
            }
        }



        //static class Atom
        //{
        //    const string ContainerName = "entry";

        //    public static readonly Func<XContainer, IEnumerable<XElement>> Descendants = x => x.Elements();
        //    public static Func<XElement, bool> Where => item => item.IsContainerName ("entry");
        //    public static Func<XElement, IItem> Select => item => new AtomItem (item);
        //    public static IEnumerable<IItem> Create(XElement element) => Descendants (element).Where (Where).Select (Select);

        //}

        //static class Rss {
        //    public static readonly Func<XContainer, IEnumerable<XElement>> Descendants =
        //        x => x.Descendants ().First (item => item.IsContainerName("channel")).Elements ();
        //    public static Func<XElement, bool> Where => item => item.IsContainerName ("item");
        //    public static Func<XElement, IItem> Select => item => new RssItem (item);
        //}

        //static class Rdf {
        //    public static readonly Func<XContainer, IEnumerable<XElement>> Descendants = x => x.Descendants ();
        //    public static Func<XElement, bool> Where => item => item.IsContainerName ("item");
        //    public static Func<XElement, IItem> Select => item => new RdfItem (item);
        //}
    }
}