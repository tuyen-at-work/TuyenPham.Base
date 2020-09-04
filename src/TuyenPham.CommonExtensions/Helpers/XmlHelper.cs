using System;
using System.Text;
using System.Xml;

namespace TuyenPham.CommonExtensions.Helpers
{
    public static class XmlHelper
    {
        internal static string Beautify(
            this XmlDocument doc,
            Encoding encoding)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = encoding
            };

            using var writer = XmlWriter.Create(sb, settings);
            doc.Save(writer);

            return sb.ToString();
        }

        public static XmlElement CreateElement(this XmlDocument xDoc, string name)
        {
            return xDoc.CreateNode(XmlNodeType.Element, name, xDoc.NamespaceURI) as XmlElement;
        }

        public static XmlElement CreateElement(this XmlElement parent, string name)
        {
            var xDoc = parent.OwnerDocument;

            if (xDoc == null)
                throw new ArgumentNullException(nameof(parent.OwnerDocument));

            var node = xDoc.CreateNode(XmlNodeType.Element, name, xDoc.NamespaceURI) as XmlElement;

            return node;
        }

        public static XmlElement AppendTo(this XmlElement node, XmlElement parentNode)
        {
            parentNode.AppendChild(node);
            return node;
        }

        public static XmlElement SetElementAttribute(this XmlElement node, string name, string value)
        {
            node.SetAttribute(name, value);
            return node;
        }
    }
}
