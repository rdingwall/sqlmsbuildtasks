using System;
using System.Data.SqlClient;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace SqlMsBuildTasks
{
    public class SqlDropDatabase : Task
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Database { get; set; }

        public override bool Execute()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    if (!DatabaseExists(connection))
                    {
                        Log.LogMessage(MessageImportance.Normal, "Database '{0}' was already dropped.", Database);
                        return true;
                    }

                    KickUsers(connection);
                    DropDatabase(connection);
                    Log.LogMessage(MessageImportance.Normal, "Dropped database '{0}'.", Database);
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }

        private void DropDatabase(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("DROP DATABASE {0};", Database);
                Log.LogMessage(MessageImportance.Low, command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        private void KickUsers(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", Database);
                Log.LogMessage(MessageImportance.Low, command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        bool DatabaseExists(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("SELECT COUNT(1) FROM SYS.DATABASES WHERE name = '{0}'", Database);
                Log.LogMessage(MessageImportance.Low, command.CommandText);
                return (int)command.ExecuteScalar() > 0;
            }
        }
    }
}
