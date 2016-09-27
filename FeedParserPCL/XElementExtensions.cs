using System.Linq;
using System.Xml.Linq;

namespace FeedParserPCL
{
    internal static class XElementExtensions {
        internal static bool IsContainerName(this XElement element, string name) =>
            element.Name.LocalName.Equals (name);

        internal static XElement Find(this XElement element, string name) =>
            element.Elements ().First (item => IsContainerName (item, name));

        internal static string FindValue(this XElement element, string name) =>
            Find (element, name).Value;
    }
}