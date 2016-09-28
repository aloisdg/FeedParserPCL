using System.Xml.Linq;
using FeedParserPCL.Item.RdfBased;

namespace FeedParserPCL.Feed.RdfBased {
    internal class Rdf : ARdfBased {
        public Rdf(XContainer element)
            : base (element.Descendants (), item => new RdfItem (item)) { }
    }
}