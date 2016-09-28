using System.Xml.Linq;

namespace FeedParserPCL.Item.RdfBased {
    internal class RdfItem : RdfBasedItem {
        private const string publishDate = "date";

        public override FeedType FeedType => FeedType.Rdf;

        public RdfItem(XElement element) : base (element, publishDate) { }
    }
}