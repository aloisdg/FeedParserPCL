using System;
using System.Xml.Linq;

namespace FeedParserPCL {
    public interface IItem {
        string Link { get; set; }
        string Title { get; set; }
        string Content { get; set; }

        // To hungarish for my taste. Can we switch to Publication or else?
        DateTime PublishDate { get; set; }
        FeedType FeedType { get; }

        IItem Parse(XElement element);
    }
}
