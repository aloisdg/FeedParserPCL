﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace FeedParserPCL.Sample
{
        internal class Program
        {
                internal static void Main()
                {
	                const string rss = "https://www.reddit.com/r/csharp.rss";
	                const string atom = "https://xkcd.com/atom.xml";
			const string rdf = "http://planetrdf.com/guide/rss.rdf";
                        var parser = new FeedParser();

			var rssItems = parser.Parse(rss, FeedType.Rss);
                        Console.WriteLine(rssItems.Count);

	                var atomItems = parser.Parse(atom, FeedType.Atom);
			Console.WriteLine(atomItems.Count);

			var rdfItems = parser.Parse(rdf, FeedType.Rdf);
			Console.WriteLine(rdfItems.Count);
                }
        }
}