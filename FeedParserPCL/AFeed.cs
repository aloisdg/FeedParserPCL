using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FeedParserPCL {
    internal abstract class AFeed : IEnumerable<IItem> {
        protected IEnumerable<IItem> Items;

        protected AFeed(IEnumerable<XElement> elements, string name, Func<XElement, IItem> select) {
            Items = elements.Where (item => item.IsContainerName (name)).Select (select);
        }

        public IEnumerator<IItem> GetEnumerator() => Items.GetEnumerator ();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator ();
    }
}
