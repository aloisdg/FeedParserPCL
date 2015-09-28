using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedParserPCL
{
	using System.Diagnostics;
	using System.Xml.Linq;

	/// <summary>
	/// A simple RSS, RDF and ATOM feed parser.
	/// </summary>
	public class FeedParser
	{
		/// <summary>
		/// Parses the given <see cref="FeedType"/> and returns a list of items/>.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <param name="feedType">The feed type.</param>
		/// <returns>A list of items</returns>
		/// <exception cref="NotSupportedException">This FeedType is not supported.</exception>
		public IList<Item> Parse(string url, FeedType feedType)
		{
			switch (feedType)
			{
				case FeedType.Rss: return ParseRss(url);
				case FeedType.Rdf: return ParseRdf(url);
				case FeedType.Atom: return ParseAtom(url);
				default: throw new NotSupportedException($"{feedType} is not supported");
			}
		}

		/// <summary>
		/// Parses an Atom feed and returns a list of items.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <returns>A list of items</returns>
		public virtual IList<Item> ParseAtom(string url)
		{
			try
			{
				var doc = XDocument.Load(url); // remove
				var entries = from item in doc.Root.Elements().Where(i => i.Name.LocalName.Equals("entry"))
					      select new Item
						{
						     FeedType = FeedType.Atom,
						     Content = item.Elements().First(i => i.Name.LocalName == "summary").Value,
						     Link = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value,
						     PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "updated").Value),
						     Title = item.Elements().First(i => i.Name.LocalName == "title").Value
						};
				return entries.ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return new List<Item>();
			}
		}

		/// <summary>
		/// Parses an RSS feed and returns a list of items.
		/// </summary>
		/// <param name="url">The url.</param>
		/// <returns>A list of items</returns>
		public virtual IList<Item> ParseRss(string url)
		{
			try
			{
				var doc = XDocument.Load(url);
				var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
					      select new Item
							     {
								     FeedType = FeedType.Rss,
								     Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
								     Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
								     PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
								     Title = item.Elements().First(i => i.Name.LocalName == "title").Value
							     };
				return entries.ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return new List<Item>();
			}
		}

		/// <summary>
		/// Parses an RDF feed and returns a list of items
		/// </summary>
		/// <param name="url">The url.</param>
		/// <returns>A list of items</returns>
		public virtual IList<Item> ParseRdf(string url)
		{
			try
			{
				var doc = XDocument.Load(url);
				// <item> is under the root
				var entries = from item in doc.Root.Descendants().Where(i => i.Name.LocalName == "item")
					      select new Item
					      {
						      FeedType = FeedType.Rdf,
						      Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
						      Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
						      PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "date").Value),
						      Title = item.Elements().First(i => i.Name.LocalName == "title").Value
					      };
				return entries.ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				return new List<Item>();
			}
		}

		/// <summary>
		/// Try to parse a Date.
		/// </summary>
		/// <param name="date">The string to parse to a DateTime</param>
		/// <returns>The date in a DateTime format or the minimum value of DateTime in case of error.</returns>
		private static DateTime ParseDate(string date)
		{
			DateTime result;
			return DateTime.TryParse(date, out result) ? result : DateTime.MinValue;
		}
	}
}
