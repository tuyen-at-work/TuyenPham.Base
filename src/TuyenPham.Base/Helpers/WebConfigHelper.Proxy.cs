using System.Xml.Linq;
using System.Xml.XPath;

namespace TuyenPham.Base.Helpers
{
    public static partial class WebConfigHelper
    {
        public static void FixProxy(this XDocument xDoc)
        {
            xDoc.XPathSelectElement("/configuration/system.net")
                .GetOrCreateElement("defaultProxy")
                .GetOrCreateElement("proxy")
                .SetAttribute("proxyaddress", "http://localhost:8888")
                .SetAttribute("bypassonlocal", "True");
        }
    }
}
