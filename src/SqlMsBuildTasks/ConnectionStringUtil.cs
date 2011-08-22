using System;
using System.Data.SqlClient;

namespace SqlMsBuildTasks
{
    public static class ConnectionStringUtil
    {
        public static string WithMasterCatalog(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException("connectionString");
            var builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" };
            return builder.ConnectionString;
        }

        public static string GetServer(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException("connectionString");
            return new SqlConnectionStringBuilder(connectionString).DataSource;
        }
    }
}
