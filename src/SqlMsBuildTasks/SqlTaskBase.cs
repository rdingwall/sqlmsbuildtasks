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
    using Microsoft.Build.Utilities;

    public abstract class SqlTaskBase : Task
    {
        /// <summary>
        /// The connection string to use when connecting to the database.
        /// </summary>
        [Required]
        public string ConnectionString { get; set; }

        protected bool DatabaseExists(SqlConnection connection, string database)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (database == null) throw new ArgumentNullException("database");

            using (var command = connection.CreateCommand())
            {
                command.CommandText = String.Format("SELECT COUNT(1) FROM sys.databases WHERE name = '{0}'", database);
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