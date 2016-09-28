using System.Linq;
using System.Xml.Linq;
using FeedParserPCL.Item.RdfBased;

namespace FeedParserPCL.Feed.RdfBased {
    internal class Rss : ARdfBased {
        private const string Name = "channel";
        public Rss(XContainer element)
            : base (element.Descendants ().First (item => item.IsContainerName (Name)).Elements ()
            , item => new RssItem (item)) { }
    }
}