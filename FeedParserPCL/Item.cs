using System;
using System.Linq;
using System.Xml.Linq;

namespace FeedParserPCL
{
        public interface IItem
        {
		string Link { get; set; }
		string Title { get; set; }
		string Content { get; set; }
		DateTime PublishDate { get;set; }
		FeedType FeedType { get; }

	        IItem Parse(XElement element);
        }

	public abstract class AItem : IItem
	{
		public abstract FeedType FeedType { get; }
		public abstract IItem Parse(XElement element);

		public string Link { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime PublishDate { get; set; }

		protected static XElement Find(XElement item, string name)
		{
			return item.Elements().First(i => i.Name.LocalName.Equals(name));
		}

		/// <summary>
		/// Try to parse a Date.
		/// </summary>
		/// <param name="date">The string to parse to a DateTime</param>
		/// <returns>The date in a DateTime format or the minimum value of DateTime in case of error.</returns>
		protected static DateTime ParseDate(string date)
		{
			DateTime result;
			return DateTime.TryParse(date, out result) ? result : DateTime.MinValue;
		}
	}

	public class AtomItem : AItem
	{
		public override FeedType FeedType { get { return FeedType.Atom; } }

		public override IItem Parse(XElement element)
		{
			return new AtomItem
			{
				Content = Find(element, "summary").Value,
				Link = Find(element, "link").Attribute("href").Value,
				PublishDate = ParseDate(Find(element, "updated").Value),
				Title = Find(element, "title").Value
			};
		}
	}

	public class RssItem : AItem
	{
		public override FeedType FeedType { get { return FeedType.Rss; } }

		public override IItem Parse(XElement element)
		{
			const string content = "description";
			const string link = "link";
			const string publishDate = "pubDate";
			const string title = "title";

			return new RssItem
			{
			     Content = Find(element, content).Value,
			     Link = Find(element, link).Value,
			     PublishDate = ParseDate(Find(element, publishDate).Value),
			     Title = Find(element, title).Value
			};
		}
	}

	public class RdfItem : AItem
	{
		public override FeedType FeedType { get { return FeedType.Rdf; } }

		public override IItem Parse(XElement element)
		{
			const string content = "description";
			const string link = "link";
			const string publishDate = "date";
			const string title = "title";

			return new RdfItem
			{
			     Content = Find(element, content).Value,
			     Link = Find(element, link).Value,
			     PublishDate = ParseDate(Find(element, publishDate).Value),
			     Title = Find(element, title).Value
			};
		}
	}

	//public abstract class AFeed : AItem
	//{
	//	public abstract string ContentName { get; }
	//	public abstract string LinkName { get; }
	//	public abstract string PublishDateName { get; }
	//	public abstract string TitleName { get; }

	//	public override FeedType FeedType
	//	{
	//		get { throw new NotImplementedException(); }
	//	}

	//	public override IItem Parse(XElement element)
	//	{
	//		return new Rss();
	//		{
	//		     Content = Find(element, ContentName).Value,
	//		     Link = Find(element, LinkName).Value,
	//		     PublishDate = ParseDate(Find(element, PublishDateName).Value),
	//		     Title = Find(element, TitleName).Value
	//		};
	//	}

	//	protected AFeed(string content, string link, string publishDate, string title)
	//	{
	//		Content = content;
	//		Link = link;
	//		PublishDate = ParseDate(publishDate);
	//		Title = title;
	//	}
	//}

        /// <summary>
        /// Represents a feed item.
        /// </summary>
        public class Item
        {
		public string Link { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime PublishDate { get;set; }
		public FeedType FeedType { get; set; }

		//public Item()
		//{
		//	Link = string.Empty;
		//	Title = string.Empty;
		//	Content = string.Empty;
		//	PublishDate = DateTime.Today;
		//	FeedType = FeedType.Rss;
		//}
        }
}
