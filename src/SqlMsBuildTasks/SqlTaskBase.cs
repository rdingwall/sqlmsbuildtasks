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

        protected bool DatabaseExists(SqlConnection connection, string database)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (database == null) throw new ArgumentNullException("database");

            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("SELECT COUNT(1) FROM SYS.DATABASES WHERE name = '{0}'", database);
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