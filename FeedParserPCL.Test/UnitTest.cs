using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace FeedParserPCL.Test {
    [TestFixture]
    public class UnitTest {
        [TestCase (".rss", FeedType.Rss, Result = 26, Description = "Test RSS")]
        [TestCase (".xml", FeedType.Atom, Result = 4, Description = "Test ATOM")]
        [TestCase (".rdf", FeedType.Rdf, Result = 10, Description = "Test RDF")]
        public int TestFromLocal(string extension, FeedType type) {
            var content = File.ReadAllText (Path.Combine (
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\File\test" + extension));
            return FeedParser.Parse (content, type).Count ();
        }
    }
}
