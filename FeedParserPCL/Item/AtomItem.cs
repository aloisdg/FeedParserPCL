using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal class AtomItem : AItem {
        private const string content = "summary";
        private const string link = "link";
        private const string linkAttribute = "href";
        private const string publishDate = "updated";

        public override FeedType FeedType => FeedType.Atom;

        public AtomItem(XElement element) : base (element, content, publishDate) {
            Link = element.Find(link).Attribute (linkAttribute).Value;
        }
    }
}
