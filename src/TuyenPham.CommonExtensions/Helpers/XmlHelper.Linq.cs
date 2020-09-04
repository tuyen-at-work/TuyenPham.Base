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
            string filePath,
            Encoding encoding)
        {
            using var fs = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            xDoc.Save(fs, encoding);
        }

        public static void Save(
            this XDocument doc,
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

        public static byte[] GetBytes(this XDocument xDoc, Encoding encoding)
        {
            using var mm = new MemoryStream();
            xDoc.Save(mm, encoding);
            return mm.ToArray();
        }

        public static string GetString(this XDocument xDoc, Encoding encoding)
        {
            using var mm = new MemoryStream();
            xDoc.Save(mm, encoding);
            return encoding.GetString(mm.ToArray());
        }

        public static string Render(this XElement node)
        {
            var encoding = new UTF8Encoding(true);
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = encoding,
                OmitXmlDeclaration = true,
                NewLineOnAttributes = true,
                CloseOutput = true
            };

            using var stream = new MemoryStream();
            using (var writer = XmlWriter.Create(stream, settings))
            {
                node.Save(writer);
            }
            return encoding.GetString(stream.ToArray());
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
