using System;
using System.Xml.Linq;

namespace FeedParserPCL {
    public interface IItem {
        string Link { get; }
        string Title { get; }
        string Content { get; }

        // To hungarish for my taste. Can we switch to Publication or else?
        DateTime PublishDate { get; }
        FeedType FeedType { get; }
    }
}
