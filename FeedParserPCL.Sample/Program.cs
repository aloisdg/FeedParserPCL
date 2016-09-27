using System;
using System.IO;

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
