using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal class RssItem : AItem {
        private const string content = "description";
        private const string link = "link";
        private const string publishDate = "pubDate";
        private const string title = "title";

        public override FeedType FeedType => FeedType.Rss;

        public override IItem Parse(XElement element) => new RssItem {
            Content = Find (element, content).Value,
            Link = Find (element, link).Value,
            PublishDate = ParseDate (Find (element, publishDate).Value),
            Title = Find (element, title).Value
        };
    }
}
