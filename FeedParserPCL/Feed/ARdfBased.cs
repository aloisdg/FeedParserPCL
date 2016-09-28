using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FeedParserPCL.Feed {
    internal abstract class ARdfBased : AFeed {
        private const string Name = "item";

        protected ARdfBased(IEnumerable<XElement> elements, Func<XElement, IItem> select)
            : base (elements, Name, select) { }
    }
}