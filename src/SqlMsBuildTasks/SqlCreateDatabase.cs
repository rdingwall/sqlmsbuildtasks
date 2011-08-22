using System;
using System.Data.SqlClient;
using Microsoft.Build.Framework;

namespace SqlMsBuildTasks
{
    /// <summary>
    /// MSBuild task to create a SQL Server database.
    /// </summary>
    /// <remarks>
    /// The provided ConnectionString's Initial Catalog/Database is set to 
    /// 'master', so you can use the same connection string as the database 
    /// you are creating.
    /// 
    /// If the database already exists, an error will be throw. Set SkipIfExists
    /// = true will disable this error (the task will simply return successfully
    /// if the database is found to already exist).
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <PropertyGroup>
    ///		<ConnectionString>Server=localhost;Integrated Security=True</ConnectionString>
    ///	</PropertyGroup>
    ///
    /// <Target Name="Drop">
    ///		<SqlCreateDatabase ConnectionString="$(ConnectionString)" Database="Northwind" />
    /// </Target>
    /// ]]></code>
    /// </example>
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
