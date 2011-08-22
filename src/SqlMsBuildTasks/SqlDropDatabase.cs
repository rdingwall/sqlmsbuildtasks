// Copyright 2011 Richard Dingwall - http://richarddingwall.name
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Data.SqlClient;
using Microsoft.Build.Framework;

namespace SqlMsBuildTasks
{
    /// <summary>
    /// MSBuild task to drop a SQL Server database.
    /// </summary>
    /// <remarks>
    /// The provided ConnectionString's Initial Catalog/Database is set to 
    /// 'master', so you can use the same connection string as the database 
    /// you are dropping. 
    /// 
    /// This task returns successfully if the database cannot be found.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <PropertyGroup>
    ///		<ConnectionString>Server=localhost;Integrated Security=True</ConnectionString>
    ///	</PropertyGroup>
    ///
    /// <Target Name="Drop">
    ///		<SqlDropDatabase ConnectionString="$(ConnectionString)" Database="Northwind" />
    /// </Target>
    /// ]]></code>
    /// </example>
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
