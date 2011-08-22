using System;
using System.Data.SqlClient;
using Microsoft.Build.Framework;

namespace SqlMsBuildTasks
{
    public class SqlDropDatabase : SqlTaskBase
    {
        public override bool Execute()
        {
            try
            {
                using (var connection = new SqlConnection(GetMasterCatalogConnectionString()))
                {
                    connection.Open();

                    if (!DatabaseExists(connection, Database))
                    {
                        Log.LogMessage(MessageImportance.Normal, "Could not find any database called {0} on {1}.", 
                            Database, GetServerName());
                        return true;
                    }

                    KickUsers(connection);
                    DropDatabase(connection);

                    Log.LogMessage(MessageImportance.Normal, "Dropped database {0} on {1}.", Database, GetServerName());

                    return true;
                }
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }



        void DropDatabase(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("DROP DATABASE {0};", Database);
                Log.LogMessage(MessageImportance.Low, command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        void KickUsers(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", Database);
                Log.LogMessage(MessageImportance.Low, command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        [Required]
        public string Database { get; set; }
    }
}
