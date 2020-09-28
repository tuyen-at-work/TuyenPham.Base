using System.Xml.Linq;
using System.Xml.XPath;
using TuyenPham.Base.Helpers;

namespace TuyenPham.Episerver
{
    public static partial class ConfigHelper
    {
        public static XElement SetEpiserverFind(
            this XDocument xDoc,
            string serviceUrl,
            string defaultIndex)
        {
            var episerverFind = xDoc.XPathSelectElement("/configuration/episerver.find")
                ?.SetAttribute("serviceUrl", serviceUrl)
                ?.SetAttribute("defaultIndex", defaultIndex);

            return episerverFind;
        }
    }
}
