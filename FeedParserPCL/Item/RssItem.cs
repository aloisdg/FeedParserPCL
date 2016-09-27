using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal class RssItem : AItem {
        public override FeedType FeedType => FeedType.Rss;

        public override IItem Parse(XElement element) {
            const string content = "description";
            const string link = "link";
            const string publishDate = "pubDate";
            const string title = "title";

            return new RssItem {
                Content = Find (element, content).Value,
                Link = Find (element, link).Value,
                PublishDate = ParseDate (Find (element, publishDate).Value),
                Title = Find (element, title).Value
            };
        }
    }
}