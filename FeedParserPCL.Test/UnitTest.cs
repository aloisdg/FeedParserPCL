using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FeedParserPCL.Test
{
	[TestFixture]
	public class UnitTest
	{
		private readonly Func<string, FeedType, int> _countItems = (content, type)
			=> new FeedParser().Parse(content, type).Count();

		[TestCase("https://www.reddit.com/r/csharp.rss", FeedType.Rss, 26, Description = "Test RSS")]
		[TestCase("https://xkcd.com/atom.xml", FeedType.Atom, 4, Description = "Test ATOM")]
		[TestCase("http://planetrdf.com/guide/rss.rdf", FeedType.Rdf, 10, Description = "Test RDF")]
		public async void Test(string url, FeedType type, int expected)
		{
			var actual = await new FeedParser().Parse(new Uri(url, UriKind.Absolute), type);
			Assert.AreEqual(actual.Count(), expected);
		}

		[TestCase(".rss", FeedType.Rss, Result = 26, Description = "Test RSS")]
		[TestCase(".xml", FeedType.Atom, Result = 4, Description = "Test ATOM")]
		[TestCase(".rdf", FeedType.Rdf, Result = 10, Description = "Test RDF")]
		public int TestFromLocal(string extension, FeedType type)
		{
			var content = File.ReadAllText(Path.Combine(
				AppDomain.CurrentDomain.BaseDirectory,
				@"..\..\File\test" + extension));
			return _countItems(content, type);
		}
	}
}
