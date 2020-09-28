using System.Xml.Linq;
using System.Xml.XPath;

namespace TuyenPham.Base.Helpers
{
    public static partial class WebConfigHelper
    {
        public static void SetProxy(this XDocument xDoc, string endpoint)
        {
            xDoc.XPathSelectElement("/configuration/system.net")
                ?.GetOrCreateElement("defaultProxy")
                ?.GetOrCreateElement("proxy")
                ?.SetAttribute("proxyaddress", endpoint)
                ?.SetAttribute("bypassonlocal", "True");
        }
    }
}
