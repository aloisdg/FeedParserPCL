using System;
using System.Xml.Linq;

namespace FeedParserPCL {
    internal abstract class AItem : IItem {
        private const string title = "title";

        public abstract FeedType FeedType { get; }

        public string Link { get; protected set; }
        public string Title { get; }
        public string Content { get; }
        public DateTime PublishDate { get; }

        protected AItem(XElement element, string content, string publishDate) {
            Content = element.FindValue (content);
            Title = element.FindValue (title);
            PublishDate = ParseDate (element.FindValue (publishDate));
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