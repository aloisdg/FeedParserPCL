using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Nito.AsyncEx;

namespace FeedParserPCL.Sample
{
        internal class Program
        {
		static void Main(string[] args)
		{
			AsyncContext.Run(() => MainAsync());
		}

		static async void MainAsync()
		{
			const string rss = "https://www.reddit.com/r/csharp.rss";
			const string atom = "https://xkcd.com/atom.xml";
			const string rdf = "http://planetrdf.com/guide/rss.rdf";

			Func<string, Uri> toUri = url => new Uri(url, UriKind.Absolute);
			var parser = new FeedParser();

			var rssItems = await parser.Parse(toUri(rss), FeedType.Rss);
			Console.WriteLine(rssItems.Count());

			var atomItems = await parser.Parse(toUri(atom), FeedType.Atom);
			Console.WriteLine(atomItems.Count());

			var rdfItems = await parser.Parse(toUri(rdf), FeedType.Rdf);
			Console.WriteLine(rdfItems.Count());
		}
        }
}
