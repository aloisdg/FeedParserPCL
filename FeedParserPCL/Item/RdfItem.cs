using System.Xml.Linq;

namespace FeedParserPCL.Item {
    internal class RdfItem : AItem {
        public override FeedType FeedType => FeedType.Rdf;

        public override IItem Parse(XElement element) {
            const string content = "description";
            const string link = "link";
            const string publishDate = "date";
            const string title = "title";

            return new RdfItem {
                Content = Find (element, content).Value,
                Link = Find (element, link).Value,
                PublishDate = ParseDate (Find (element, publishDate).Value),
                Title = Find (element, title).Value
            };
        }
    }
}