using System.Xml.Linq;
using System.Xml.XPath;
using TuyenPham.Base.Helpers;

namespace TuyenPham.Episerver
{
    public static partial class WebConfigHelper
    {
        public static XElement SetEpiserverFind(
            this XDocument xDoc,
            string serviceUrl,
            string defaultIndex)
        {
            return xDoc.XPathSelectElement("/configuration/episerver.find")
                ?.SetAttribute("serviceUrl", serviceUrl)
                ?.SetAttribute("defaultIndex", defaultIndex);
        }

        public static void DisableErrorHandler(this XDocument xDoc)
        {
            xDoc
                .XPathSelectElement("/configuration/system.webServer/httpErrors")
                ?.Remove();
        }
    }
}
