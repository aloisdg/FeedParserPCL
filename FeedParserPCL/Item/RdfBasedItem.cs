using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal abstract class RdfBasedItem : AItem {
        private const string content = "description";
        private const string link = "link";

        public abstract override FeedType FeedType { get; }

        protected RdfBasedItem(XElement element, string publishDate)
            : base (element, content, publishDate) {
            Link = element.FindValue (link);
        }
    }
}