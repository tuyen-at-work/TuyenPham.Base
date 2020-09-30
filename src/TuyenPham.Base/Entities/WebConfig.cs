using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TuyenPham.Base.Helpers;

namespace TuyenPham.Base.Entities
{
    public partial class WebConfig
    {
        private readonly FileInfo _webConfigFile;
        private readonly XDocument _xDoc;
        private readonly string _originalXml;

        public XElement ConnectionStrings => _xDoc.XPathSelectElement("/configuration/connectionStrings");

        private static readonly XmlWriterSettings _xmlWriterSettings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(true),
            Indent = true,
            IndentChars = "  "
        };

        private WebConfig(FileInfo webConfigFile, XDocument xDoc)
        {
            _webConfigFile = webConfigFile;
            _xDoc = xDoc;

            _originalXml = xDoc.GetString(_xmlWriterSettings);
        }

        public bool IsDirty => !_xDoc.GetString(_xmlWriterSettings).Equals(_originalXml);

        public async Task SaveIfDirtyAsync()
        {
            if (!IsDirty)
                return;

            var xmlBytes = _xDoc.GetBytes(_xmlWriterSettings);
            await File.WriteAllBytesAsync(_webConfigFile.FullName, xmlBytes);
        }

        public WebConfig SetProxy(string endpoint)
        {
            _xDoc.XPathSelectElement("/configuration/system.net")
                ?.GetOrCreateElement("defaultProxy")
                ?.GetOrCreateElement("proxy")
                ?.SetAttribute("proxyaddress", endpoint)
                ?.SetAttribute("bypassonlocal", "True");

            return this;
        }

        public static async Task<WebConfig> FromFileAsync(FileInfo webConfigFile)
        {
            if (!webConfigFile.Exists)
                return null;

            var config = await File.ReadAllTextAsync(webConfigFile.FullName);
            var xDoc = XDocument.Parse(config);

            return new WebConfig(webConfigFile, xDoc);
        }
    }
}
