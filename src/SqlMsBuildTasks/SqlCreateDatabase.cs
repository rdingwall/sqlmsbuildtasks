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

namespace SqlMsBuildTasks
{
    using System;
    using System.Data.SqlClient;
    using Microsoft.Build.Framework;

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
        /// <summary>
        /// The name of the database to create.
        /// </summary>
        [Required]
        public string Database { get; set; }

        public override bool Execute()
        {
            try
            {
                using (var connection = new SqlConnection(GetMasterCatalogConnectionString()))
                {
                    connection.Open();

                    CreateDatabase(connection);

                    Log.LogMessage(MessageImportance.Normal, "Created empty database {0} on {1}.", Database, GetServerName());
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
