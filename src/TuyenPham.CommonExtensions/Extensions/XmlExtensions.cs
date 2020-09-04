using System;
using System.IO;
using System.Text;
using System.Xml;

namespace TuyenPham.Base.Extensions
{
    public static class XmlExtensions
    {
        public static void Save(
            this XmlDocument xDoc,
            string filePath,
            Encoding encoding)
        {
            using var fs = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            xDoc.Save(fs, encoding);
        }

        public static void Save(
            this XmlDocument doc,
            Stream stream,
            Encoding encoding)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = encoding
            };

            using var writer = XmlWriter.Create(stream, settings);
            doc.Save(writer);
        }

        public static byte[] GetBytes(this XmlDocument xDoc, Encoding encoding)
        {
            using var mm = new MemoryStream();
            xDoc.Save(mm, encoding);
            return mm.ToArray();
        }

        public static string GetString(this XmlDocument xDoc, Encoding encoding)
        {
            using var mm = new MemoryStream();
            xDoc.Save(mm, encoding);
            return encoding.GetString(mm.ToArray());
        }

        public static XmlElement CreateElement(this XmlDocument xDoc, string name)
        {
            return xDoc.CreateNode(XmlNodeType.Element, name, xDoc.NamespaceURI) as XmlElement;
        }

        public static XmlElement CreateElement(this XmlNode parent, string name)
        {
            var xDoc = parent.OwnerDocument;

            if (xDoc == null)
                throw new ArgumentNullException(nameof(parent.OwnerDocument));

            var node = xDoc.CreateNode(XmlNodeType.Element, name, xDoc.NamespaceURI) as XmlElement;

            return node;
        }

        public static XmlElement AppendTo(this XmlElement node, XmlNode parentNode)
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
