using System;
using System.Data.SqlClient;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace SqlMsBuildTasks
{
    public class SqlCreateDatabase : Task
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Database { get; set; }

        public override bool Execute()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionStringUtil.WithMasterCatalog(ConnectionString)))
                {
                    connection.Open();
                    CreateDatabase(connection);

                    Log.LogMessage(MessageImportance.Normal, "Created empty database {0} on {1}.", 
                        Database, ConnectionStringUtil.GetServer(ConnectionString));

                    return true;
                }
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }

        void CreateDatabase(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("CREATE DATABASE {0};", Database);
                Log.LogMessage(MessageImportance.Low, command.CommandText);
                command.ExecuteNonQuery();
            }
        }
    }
}
