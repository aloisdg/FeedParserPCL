using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

		/// <summary>
		/// Parses an Atom feed and returns a list of items.
		/// </summary>
		/// <param name="content"></param>
		/// <returns>A list of items</returns>
		public virtual IEnumerable<Item> ParseAtom(string content)
		{
			try
			{
				var doc = XDocument.Parse(content);
				return from item in doc.Root.Elements().Where(i => i.Name.LocalName.Equals("entry"))
					      select new Item
						{
						     FeedType = FeedType.Atom,
						     Content = item.Elements().First(i => i.Name.LocalName == "summary").Value,
						     Link = item.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value,
						     PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "updated").Value),
						     Title = item.Elements().First(i => i.Name.LocalName == "title").Value
						};
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
		/// <param name="content"></param>
		/// <param name="url">The url.</param>
		/// <returns>A list of items</returns>
		public virtual IEnumerable<Item> ParseRss(string content)
		{
			try
			{
				var doc = XDocument.Parse(content);
				return from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
					      select new Item
							     {
								     FeedType = FeedType.Rss,
								     Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
								     Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
								     PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
								     Title = item.Elements().First(i => i.Name.LocalName == "title").Value
							     };
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
		/// <param name="content"></param>
		/// <returns>A list of items</returns>
		public virtual IEnumerable<Item> ParseRdf(string content)
		{
			try
			{
				var doc = XDocument.Parse(content);
				// <item> is under the root
				return from item in doc.Root.Descendants().Where(i => i.Name.LocalName == "item")
					      select new Item
					      {
						      FeedType = FeedType.Rdf,
						      Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
						      Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
						      PublishDate = ParseDate(item.Elements().First(i => i.Name.LocalName == "date").Value),
						      Title = item.Elements().First(i => i.Name.LocalName == "title").Value
					      };
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
