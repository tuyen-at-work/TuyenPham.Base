using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TuyenPham.Base.Helpers
{
    public static partial class WebConfigHelper
    {
        public static XDocument SetConnectionString(
            this XDocument xDoc,
            string name,
            string connectionString,
            params KeyValuePair<string, string>[] attributes)
        {
            var connectionStrings = xDoc.XPathSelectElement("/configuration/connectionStrings");

            var conn = connectionStrings
                .GetOrCreateElement(
                    "add",
                    new KeyValuePair<string, string>("name", name));

            conn.SetAttribute("connectionString", connectionString);

            foreach (var (k, v) in attributes)
                conn.SetAttribute(k, v);

            return xDoc;
        }

        public static XDocument SetSqlConnectionString(
            this XDocument xDoc,
            string name,
            string connectionString)
        {
            xDoc.SetConnectionString(
                name,
                connectionString,
                new KeyValuePair<string, string>("providerName", "System.Data.SqlClient"));

            return xDoc;
        }
    }
}
