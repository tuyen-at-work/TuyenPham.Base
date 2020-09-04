using System.IO;
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

        public static string GetString(
            this XElement node,
            XmlWriterSettings settings = null)
        {
            settings ??= new XmlWriterSettings();

            using var stream = new MemoryStream();
            using var writer = XmlWriter.Create(stream, settings);
            node.Save(writer);
            return settings.Encoding.GetString(stream.ToArray());
        }

        public static XElement AddElement(this XElement parent, string name)
        {
            var node = new XElement(name);
            parent.Add(node!);

            return node;
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
