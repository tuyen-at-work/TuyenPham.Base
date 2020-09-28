using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TuyenPham.Base.Helpers
{
    public static partial class XmlHelper
    {
        public static void Save(
            this XDocument xDoc,
            Stream stream,
            XmlWriterSettings settings = null)
        {
            settings ??= new XmlWriterSettings();

            using var writer = XmlWriter.Create(stream, settings);
            xDoc.Save(writer);
        }

        public static void Save(
            this XDocument xDoc,
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
            this XDocument xDoc,
            XmlWriterSettings settings = null)
        {
            using var mm = new MemoryStream();
            xDoc.Save(mm, settings);

            return mm.ToArray();
        }

        public static string GetString(
            this XDocument xDoc,
            XmlWriterSettings settings = null)
        {
            settings ??= new XmlWriterSettings();

            var bytes = xDoc.GetBytes(settings);
            return settings.Encoding.GetString(bytes);
        }

        public static void Save(
            this XElement xElement,
            Stream stream,
            XmlWriterSettings settings = null)
        {
            settings ??= new XmlWriterSettings();

            using var writer = XmlWriter.Create(stream, settings);
            xElement.Save(writer);
        }

        public static byte[] GetBytes(
            this XElement xElement,
            XmlWriterSettings settings = null)
        {
            using var mm = new MemoryStream();
            xElement.Save(mm, settings);

            return mm.ToArray();
        }

        public static string GetString(
            this XElement xElement,
            XmlWriterSettings settings = null)
        {
            settings ??= new XmlWriterSettings();

            var bytes = xElement.GetBytes(settings);
            return settings.Encoding.GetString(bytes);
        }

        public static XElement AddElement(this XElement parent, string name)
        {
            var node = new XElement(name);
            parent.Add(node!);

            return node;
        }

        public static XElement GetOrCreateElement(
            this XElement parent,
            string name,
            params KeyValuePair<string, string>[] attributes)
        {
            var element = parent
                .Elements(name)
                .FirstOrDefault(e => attributes.All(a => e.Attribute(a.Key)?.Value == a.Value));

            if (element == null)
            {
                element = parent.AddElement(name);

                foreach (var (k, v) in attributes)
                    element.SetAttribute(k, v);
            }

            return element;
        }

        public static XElement RemoveChildren(this XElement parent)
        {
            parent.RemoveNodes();

            return parent;
        }

        public static void SetText(this XElement element, string text)
        {
            element.SetValue(text);
        }

        public static XElement AppendTo(this XElement node, XElement parentNode)
        {
            parentNode.Add(node);
            return node;
        }

        public static XElement SetAttribute(this XElement node, string name, object value)
        {
            node.SetAttributeValue(name, value);
            return node;
        }
    }
}
