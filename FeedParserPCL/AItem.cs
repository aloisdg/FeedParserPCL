using System;
using System.Linq;
using System.Xml.Linq;

namespace FeedParserPCL {
    internal abstract class AItem : IItem {
        public abstract FeedType FeedType { get; }
        public abstract IItem Parse(XElement element);

        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        protected static XElement Find(XElement item, string name) {
            return item.Elements ().First (i => i.Name.LocalName.Equals (name));
        }

        /// <summary>
        /// Try to parse a Date.
        /// </summary>
        /// <param name="date">The string to parse to a DateTime</param>
        /// <returns>The date in a DateTime format or the minimum value of DateTime in case of error.</returns>
        protected static DateTime ParseDate(string date) {
            DateTime result;
            return DateTime.TryParse (date, out result) ? result : DateTime.MinValue;
        }
    }
}