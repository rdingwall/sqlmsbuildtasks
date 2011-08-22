using System;
using System.Data.SqlClient;
using Microsoft.Build.Framework;

namespace SqlMsBuildTasks
{
    public class SqlCreateDatabase : SqlTaskBase
    {
        public bool SkipIfExists { get; set; }

        [Required]
        public string Database { get; set; }

        public override bool Execute()
        {
            try
            {
                using (var connection = new SqlConnection(GetMasterCatalogConnectionString()))
                {
                    connection.Open();

                    if (SkipIfExists && DatabaseExists(connection, Database))
                    {
                        Log.LogMessage(MessageImportance.Normal, "A database called {0} already exists on {1}.",
                            Database, GetServerName());
                        return true;
                    }
                    
                    CreateDatabase(connection);

                    Log.LogMessage(MessageImportance.Normal, "Created empty database {0} on {1}.", 
                        Database, GetServerName());

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
