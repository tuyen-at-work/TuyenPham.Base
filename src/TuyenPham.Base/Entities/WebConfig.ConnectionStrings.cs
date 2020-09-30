using System.Collections.Generic;
using TuyenPham.Base.Helpers;

namespace TuyenPham.Base.Entities
{
    public partial class WebConfig
    {
        public WebConfig SetConnectionString(
            string name,
            string connectionString,
            params KeyValuePair<string, string>[] attributes)
        {
            if (ConnectionStrings == null)
                return this;

            var conn = ConnectionStrings
                .GetOrCreateElement(
                    "add",
                    new KeyValuePair<string, string>("name", name));

            conn.SetAttribute("connectionString", connectionString);

            foreach (var (k, v) in attributes)
                conn.SetAttribute(k, v);

            return this;
        }

        public WebConfig SetSqlConnectionString(
            string name,
            string connectionString)
        {
            return SetConnectionString(
                name,
                connectionString,
                new KeyValuePair<string, string>("providerName", "System.Data.SqlClient"));
        }

        public WebConfig SetSqlConnectionString(
            string name,
            string dataSource,
            string initialCatalog,
            string userId,
            string password)
        {
            var connectionString =
                $"Data Source={dataSource};Initial Catalog={initialCatalog};Connection Timeout=60;Integrated Security=false;User Id={userId};Password={password};MultipleActiveResultSets=True";

            return SetConnectionString(
                name,
                connectionString,
                new KeyValuePair<string, string>("providerName", "System.Data.SqlClient"));
        }

        public WebConfig SetAzureBlobConnectionString(
            string name,
            string connectionString)
        {
            return SetConnectionString(
                name,
                connectionString);
        }

        public WebConfig SetAzureBlobConnectionString(
            string name,
            string accountName,
            string accountKey,
            string defaultEndpointsProtocol = "https",
            string endpointSuffix = "core.windows.net")
        {
            var connectionString = $"AccountName={accountName};AccountKey={accountKey};DefaultEndpointsProtocol={defaultEndpointsProtocol};EndpointSuffix={endpointSuffix};";

            return SetConnectionString(
                name,
                connectionString);
        }
    }
}
