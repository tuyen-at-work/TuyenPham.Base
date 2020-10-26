using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using TuyenPham.Base.Helpers;

namespace TuyenPham.Episerver
{
    public static partial class WebConfigHelper
    {
        public static XElement GetBlobProvidersElement(this XDocument xDoc)
        {
            return xDoc
                .XPathSelectElement("/configuration/episerver.framework/blob");
        }

        public static XElement SetDefaultProvider(this XElement element, string providerName)
        {
            element.SetAttribute("defaultProvider", providerName);

            return element;
        }

        public static XElement AddOrUpdateAzureBlob(
            this XElement element,
            string name,
            string connectionName,
            string container)
        {
            var child = element
                .GetOrCreateElement("providers")
                .GetOrCreateElement("add", new KeyValuePair<string, string>("name", name));

            child
                .SetAttribute("name", name)
                .SetAttribute("type", "EPiServer.Azure.Blobs.AzureBlobProvider,EPiServer.Azure")
                .SetAttribute("connectionStringName", connectionName)
                .SetAttribute("container", container);

            return element;
        }

        public static XElement AddOrUpdateFileBlob(
            this XElement element,
            string name,
            string path)
        {
            var child = element
                .GetOrCreateElement("providers")
                .GetOrCreateElement("add", new KeyValuePair<string, string>("name", name));

            child
                .SetAttribute("type", "EPiServer.Framework.Blobs.FileBlobProvider, EPiServer.Framework")
                .SetAttribute("path", path);

            return element;
        }
    }
}
