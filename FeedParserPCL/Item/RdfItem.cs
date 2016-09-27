using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal class RdfItem : AItem {
        private const string content = "description";
        private const string link = "link";
        private const string publishDate = "date";
        private const string title = "title";

        public override FeedType FeedType => FeedType.Rdf;

        public override IItem Parse(XElement element) => new RdfItem {
            Content = Find (element, content).Value,
            Link = Find (element, link).Value,
            PublishDate = ParseDate (Find (element, publishDate).Value),
            Title = Find (element, title).Value
        };
    }
}