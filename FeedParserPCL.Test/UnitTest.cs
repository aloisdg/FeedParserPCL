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
		[TestCase("https://www.reddit.com/r/csharp.rss", FeedType.Rss, Description = "Test RSS")]
		[TestCase("https://xkcd.com/atom.xml", FeedType.Atom, Description = "Test ATOM")]
		[TestCase("http://planetrdf.com/guide/rss.rdf", FeedType.Rdf, Description = "Test RDF")]
		public async void Test(string url, FeedType type)
		{
			var actual = await FeedParser.Parse(new Uri(url, UriKind.Absolute), type);
			Assert.IsTrue(actual.Any());
		}

		[TestCase(".rss", FeedType.Rss, Result = 26, Description = "Test RSS")]
		[TestCase(".xml", FeedType.Atom, Result = 4, Description = "Test ATOM")]
		[TestCase(".rdf", FeedType.Rdf, Result = 10, Description = "Test RDF")]
		public int TestFromLocal(string extension, FeedType type)
		{
			var content = File.ReadAllText(Path.Combine(
				AppDomain.CurrentDomain.BaseDirectory,
				@"..\..\File\test" + extension));
			return  FeedParser.Parse(content, type).Count();
		}
	}
}
