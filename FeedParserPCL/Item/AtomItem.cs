using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal class AtomItem : AItem {
        private const string content = "summary";
        private const string link = "link";
        private const string linkAttribute = "href";
        private const string publishDate = "updated";
        private const string title = "title";

        public override FeedType FeedType => FeedType.Atom;

        public override IItem Parse(XElement element) => new AtomItem {
            Content = Find (element, content).Value,
            Link = Find (element, link).Attribute (linkAttribute).Value,
            PublishDate = ParseDate (Find (element, publishDate).Value),
            Title = Find (element, title).Value
        };
    }
}
