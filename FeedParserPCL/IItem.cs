using System;
using System.Xml.Linq;

namespace FeedParserPCL {
    public interface IItem {
        string Link { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        DateTime PublishDate { get; set; }
        FeedType FeedType { get; }

        IItem Parse(XElement element);
    }

    //public abstract class AFeed : AItem
    //{
    //	public abstract string ContentName { get; }
    //	public abstract string LinkName { get; }
    //	public abstract string PublishDateName { get; }
    //	public abstract string TitleName { get; }

    //	public override FeedType FeedType
    //	{
    //		get { throw new NotImplementedException(); }
    //	}

    //	public override IItem Parse(XElement element)
    //	{
    //		return new Rss();
    //		{
    //		     Content = Find(element, ContentName).Value,
    //		     Link = Find(element, LinkName).Value,
    //		     PublishDate = ParseDate(Find(element, PublishDateName).Value),
    //		     Title = Find(element, TitleName).Value
    //		};
    //	}

    //	protected AFeed(string content, string link, string publishDate, string title)
    //	{
    //		Content = content;
    //		Link = link;
    //		PublishDate = ParseDate(publishDate);
    //		Title = title;
    //	}
    //}

    ///// <summary>
    ///// Represents a feed item.
    ///// </summary>
    //public class Item
    //{
    //	public string Link { get; set; }
    //	public string Title { get; set; }
    //	public string Content { get; set; }
    //	public DateTime PublishDate { get;set; }
    //	public FeedType FeedType { get; set; }

    //	//public Item()
    //	//{
    //	//	Link = string.Empty;
    //	//	Title = string.Empty;
    //	//	Content = string.Empty;
    //	//	PublishDate = DateTime.Today;
    //	//	FeedType = FeedType.Rss;
    //	//}
    //}
}
