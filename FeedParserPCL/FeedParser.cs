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
		public async Task<IEnumerable<IItem>> Parse(Uri url, FeedType feedType)
		{
			using (var httpClient = new HttpClient())
			{
				var content = await httpClient.GetStringAsync(url).ConfigureAwait(false);
				return Parse(content, feedType);
			}
		}

		public IEnumerable<IItem> Parse(string content, FeedType feedType)
		{
			var root = XDocument.Parse(content).Root;
			if (root == null)
				throw new FormatException("root");
			return ParseXml(root, feedType);
		}

		private static IEnumerable<IItem> ParseXml(XContainer root, FeedType feedType)
		{
			switch (feedType)
			{
				case FeedType.Rss: return ParseRss(root);
				case FeedType.Rdf: return ParseRdf(root);
				case FeedType.Atom: return ParseAtom(root);
				default: throw new NotSupportedException(feedType + " is not supported");
			}
		}

		/// <summary>
		/// Parses an Atom feed and returns a list of items.
		/// </summary>
		/// <returns>A list of items</returns>
		private static IEnumerable<IItem> ParseAtom(XContainer root)
		{
			return root.Elements()
				.Where(i => i.Name.LocalName.Equals("entry"))
				.Select(item => new AtomItem().Parse(item));
		}

		/// <summary>
		/// Parses an RSS feed and returns a list of items.
		/// </summary>
		/// <returns>A list of items</returns>
		private static IEnumerable<IItem> ParseRss(XContainer root)
		{
			return root.Descendants()
				.First(i => i.Name.LocalName == "channel")
				.Elements().Where(i => i.Name.LocalName == "item")
				.Select(item => new RssItem().Parse(item));
		}

		/// <summary>
		/// Parses an RDF feed and returns a list of items
		/// </summary>
		/// <returns>A list of items</returns>
		private static IEnumerable<IItem> ParseRdf(XContainer root)
		{
			return root.Descendants()
				.Where(i => i.Name.LocalName == "item")
				.Select(item => new RdfItem().Parse(item));
		}
	}
}