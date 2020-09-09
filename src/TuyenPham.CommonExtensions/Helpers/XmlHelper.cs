using System;
using System.IO;
using System.Text;
using System.Xml;

namespace TuyenPham.Base.Helpers
{
    public static partial class XmlHelper
    {
        public static void Save(
            this XmlDocument xDoc,
            Stream stream,
            XmlWriterSettings settings = null)
        {
            settings ??= new XmlWriterSettings();
            settings.CloseOutput = true;

            using var writer = XmlWriter.Create(stream, settings);
            xDoc.Save(writer);
        }

        public static void Save(
            this XmlDocument xDoc,
            string filePath,
            XmlWriterSettings settings = null)
        {
            using var fs = File.Open(
                filePath,
                FileMode.Create,
                FileAccess.ReadWrite,
                FileShare.Read);

            xDoc.Save(fs, settings);
        }

        public static byte[] GetBytes(
            this XmlDocument xDoc,
            XmlWriterSettings settings = null)
        {
            using var mm = new MemoryStream();
            xDoc.Save(mm, settings);

            return mm.ToArray();
        }

        public static string GetString(
            this XmlDocument xDoc,
            XmlWriterSettings settings = null)
        {
            settings ??= new XmlWriterSettings();

            var bytes = xDoc.GetBytes(settings);
            return settings.Encoding.GetString(bytes);
        }

        public static XmlElement CreateElement(this XmlDocument xDoc, string name)
        {
            return xDoc.CreateNode(XmlNodeType.Element, name, xDoc.NamespaceURI) as XmlElement;
        }

        public static XmlElement AddElement(this XmlNode parent, string name)
        {
            var xDoc = parent.OwnerDocument;

            var node = xDoc!.CreateNode(XmlNodeType.Element, name, xDoc.NamespaceURI) as XmlElement;
            parent.AppendChild(node!);

            return node;
        }

        public static void SetText(this XmlElement element, string text)
        {
            var xDoc = element.OwnerDocument;

            var node = xDoc!.CreateTextNode(text);
            element.AppendChild(node);
        }

        public static XmlElement AppendTo(
            this XmlElement node,
            XmlNode parentNode)
        {
            parentNode.AppendChild(node);
            return node;
        }

        public static XmlElement SetElementAttribute
            (this XmlElement node,
            string name,
            string value)
        {
            node.SetAttribute(name, value);
            return node;
        }
    }
}
