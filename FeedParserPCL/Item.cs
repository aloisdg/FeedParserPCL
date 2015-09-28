using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedParserPCL
{
        /// <summary>
        /// Represents a feed item.
        /// </summary>
        public class Item
        {
		public string Link { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime PublishDate { get; set; }
		public FeedType FeedType { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FeedParserPCL"/> class.
		/// </summary>
		public Item()
		{
			Link = string.Empty;
			Title = string.Empty;
			Content = string.Empty;
			PublishDate = DateTime.Today;
			FeedType = FeedType.Rss;
		}
        }
}
