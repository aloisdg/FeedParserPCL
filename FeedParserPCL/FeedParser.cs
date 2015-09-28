using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FeedParserPCL
{
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
		public async Task<IEnumerable<Item>> Parse(Uri url, FeedType feedType)
		{
			using (var httpClient = new HttpClient())
			{
				var content = await httpClient.GetStringAsync(url).ConfigureAwait(false);
				return Parse(content, feedType);
			}
		}

		public IEnumerable<Item> Parse(string content, FeedType feedType)
		{
			switch (feedType)
			{
				case FeedType.Rss: return ParseRss(content);
				case FeedType.Rdf: return ParseRdf(content);
				case FeedType.Atom: return ParseAtom(content);
				default: throw new NotSupportedException(feedType + " is not supported");
			}
		}

		private static readonly Func<XElement, string, XElement> Find =
			(item, name) => item.Elements().First(i => i.Name.LocalName.Equals(name));

		/// <summary>
		/// Parses an Atom feed and returns a list of items.
		/// </summary>
		/// <param name="content"></param>
		/// <returns>A list of items</returns>
		private static IEnumerable<Item> ParseAtom(string content)
		{
			var root = XDocument.Parse(content).Root;
			if (root == null)
				throw new FormatException("root");
			return from item in root
				       .Elements().Where(i => i.Name.LocalName.Equals("entry"))
			       select new Item
			       {
				       FeedType = FeedType.Atom,
				       Content = Find(item, "summary").Value,
				       Link = Find(item, "link").Attribute("href").Value,
				       PublishDate = ParseDate(Find(item, "updated").Value),
				       Title = Find(item, "title").Value
			       };
		}

		/// <summary>
		/// Parses an RSS feed and returns a list of items.
		/// </summary>
		/// <param name="content"></param>
		/// <returns>A list of items</returns>
		private static IEnumerable<Item> ParseRss(string content)
		{
			var root = XDocument.Parse(content).Root;
			if (root == null)
				throw new FormatException("root");
			return from item in root
				.Descendants().First(i => i.Name.LocalName == "channel")
				.Elements().Where(i => i.Name.LocalName == "item")
			       select new Item
			       {
				       FeedType = FeedType.Rss,
				       Content = Find(item, "description").Value,
				       Link = Find(item, "link").Value,
				       PublishDate = ParseDate(Find(item, "pubDate").Value),
				       Title = Find(item, "title").Value
			       };
		}

		/// <summary>
		/// Parses an RDF feed and returns a list of items
		/// </summary>
		/// <param name="content"></param>
		/// <returns>A list of items</returns>
		private static IEnumerable<Item> ParseRdf(string content)
		{
			var root = XDocument.Parse(content).Root;
			if (root == null)
				throw new FormatException("root");
			// <item> is under the root
			return from item in root
				       .Descendants().Where(i => i.Name.LocalName == "item")
			       select new Item
			       {
				       FeedType = FeedType.Rdf,
				       Content = Find(item, "description").Value,
				       Link = Find(item, "link").Value,
				       PublishDate = ParseDate(Find(item, "date").Value),
				       Title = Find(item, "title").Value
			       };
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
