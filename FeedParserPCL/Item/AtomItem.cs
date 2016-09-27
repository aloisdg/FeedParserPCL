using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal class AtomItem : AItem {
        public override FeedType FeedType => FeedType.Atom;

        public override IItem Parse(XElement element) {
            const string content = "summary";
            const string link = "link";
            const string linkAttribute = "href";
            const string publishDate = "updated";
            const string title = "title";

            return new AtomItem {
                Content = Find (element, content).Value,
                Link = Find (element, link).Attribute (linkAttribute).Value,
                PublishDate = ParseDate (Find (element, publishDate).Value),
                Title = Find (element, title).Value
            };
        }
    }
}