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
        private FileInfo FileInfo { get; }
        public XDocument XDocument { get; }
        private readonly string _originalXml;

        public XElement ConnectionStrings => XDocument.XPathSelectElement("/configuration/connectionStrings");

        private static readonly XmlWriterSettings _xmlWriterSettings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(true),
            Indent = true,
            IndentChars = "  "
        };

        private WebConfig(FileInfo fileInfo, XDocument xDocument)
        {
            XDocument = xDocument;
            FileInfo = fileInfo;

            _originalXml = xDocument.GetString(_xmlWriterSettings);
        }

        public bool IsDirty => !XDocument.GetString(_xmlWriterSettings).Equals(_originalXml);

        public async Task SaveIfDirtyAsync()
        {
            if (!IsDirty)
                return;

            var xmlBytes = XDocument.GetBytes(_xmlWriterSettings);
            await File.WriteAllBytesAsync(FileInfo.FullName, xmlBytes);
        }

        public WebConfig SetProxy(string endpoint)
        {
            XDocument.XPathSelectElement("/configuration/system.net")
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
