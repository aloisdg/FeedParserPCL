using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using Nito.AsyncEx;

namespace FeedParserPCL.Sample {
    internal class Program {
        private static void Main() {

            var content = File.ReadAllText (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, @"..\..\File\test.rss"));

            var feed = FeedParser.Parse (content, FeedType.Rss);
            foreach (var item in feed)
                Console.WriteLine ($"{item.PublishDate} {item.Title}");

            Console.ReadLine ();
        }
    }
}
