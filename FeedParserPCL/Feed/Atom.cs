using System.Xml.Linq;
using FeedParserPCL.Item;

namespace FeedParserPCL.Feed {
    internal class Atom : AFeed {
        private const string Name = "entry";

        public Atom(XContainer element)
            : base (element.Elements (), Name, item => new AtomItem (item)) { }
    }
}