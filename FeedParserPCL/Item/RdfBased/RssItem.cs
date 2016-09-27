using System.Xml.Linq;

namespace FeedParserPCL.Item.RdfBased {
    internal class RssItem : RdfBasedItem {
        private const string publishDate = "pubDate";

        public override FeedType FeedType => FeedType.Rss;

        public RssItem(XElement element) : base (element, publishDate) {}
    }
}
