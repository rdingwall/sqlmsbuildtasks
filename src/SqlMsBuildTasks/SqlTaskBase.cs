using System;
using System.Data.SqlClient;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace SqlMsBuildTasks
{
    public abstract class SqlTaskBase : Task
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Database { get; set; }

        protected bool DatabaseExists(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("SELECT COUNT(1) FROM SYS.DATABASES WHERE name = '{0}'", Database);
                Log.LogMessage(MessageImportance.Low, command.CommandText);
                return (int)command.ExecuteScalar() > 0;
            }
        }

        protected string GetMasterCatalogConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString) { InitialCatalog = "master" };
            return builder.ConnectionString;
        }

        protected string GetServerName()
        {
            return new SqlConnectionStringBuilder(ConnectionString).DataSource;
        }
    }
}