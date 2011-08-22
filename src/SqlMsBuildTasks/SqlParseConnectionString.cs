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
    /// MSBuild task to parse individual keys out of a SQL Server connection string.
    /// </summary>
    /// <remarks>
    /// You can use all the properties exposed on <see 
    /// cref="System.Data.SqlClient.SqlConnectionStringBuilder" />.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <PropertyGroup>
    ///		<ConnectionString>Server=localhost;Database=NorthWind;Integrated Security=True</ConnectionString>
    ///	</PropertyGroup>
    ///
    ///	<SqlParseConnectionString ConnectionString="$(ConnectionString)">
    ///      <Output PropertyName="myDb" TaskParameter="InitialCatalog" />
    ///      <Output PropertyName="myServer" TaskParameter="DataSource" />
    ///      <Output PropertyName="myTimeout" TaskParameter="ConnectTimeout" />
    /// </SqlParseConnectionString>
    /// <Message Text="Parsed the $(myDb) database on server $(myServer) with timeout = $(myTimeout)." />
    /// </Target>
    /// ]]></code>
    /// </example>
    public class SqlParseConnectionString : SqlTaskBase
    {
        [Output]
        public string DataSource { get; private set; }

        [Output]
        public string InitialCatalog { get; private set; }

        [Output]
        public string WorkstationID { get; private set; }

        [Output]
        public bool UserInstance { get; private set; }

        [Output]
        public string UserID { get; private set; }

        [Output]
        public string TypeSystemVersion { get; private set; }

        [Output]
        public bool TrustServerCertificate { get; private set; }

        [Output]
        public string TransactionBinding { get; private set; }

        [Output]
        public bool Replication { get; private set; }

        [Output]
        public bool Pooling { get; private set; }

        [Output]
        public bool PersistSecurityInfo { get; private set; }

        [Output]
        public string Password { get; private set; }

        [Output]
        public int PacketSize { get; private set; }

        [Output]
        public string NetworkLibrary { get; private set; }

        [Output]
        public bool MultipleActiveResultSets { get; private set; }

        [Output]
        public int MinPoolSize { get; private set; }

        [Output]
        public int MaxPoolSize { get; private set; }

        [Output]
        public int LoadBalanceTimeout { get; private set; }

        [Output]
        public bool IntegratedSecurity { get; private set; }

        [Output]
        public string FailoverPartner { get; private set; }

        [Output]
        public bool Enlist { get; private set; }

        [Output]
        public bool Encrypt { get; private set; }

        [Output]
        public string CurrentLanguage { get; private set; }

        [Output]
        public bool ContextConnection { get; private set; }

        [Output]
        public int ConnectTimeout { get; private set; }

        [Output]
        public string AttachDBFilename { get; private set; }

        [Output]
        public bool AsynchronousProcessing { get; private set; }

        [Output]
        public string ApplicationName { get; private set; }

        public override bool Execute()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(ConnectionString);

                DataSource = builder.DataSource;
                InitialCatalog = builder.InitialCatalog;
                ApplicationName = builder.ApplicationName;
                AsynchronousProcessing = builder.AsynchronousProcessing;
                AttachDBFilename = builder.AttachDBFilename;
                ConnectTimeout = builder.ConnectTimeout;
                ContextConnection = builder.ContextConnection;
                CurrentLanguage = builder.CurrentLanguage;
                Encrypt = builder.Encrypt;
                Enlist = builder.Enlist;
                FailoverPartner = builder.FailoverPartner;
                IntegratedSecurity = builder.IntegratedSecurity;
                LoadBalanceTimeout = builder.LoadBalanceTimeout;
                MaxPoolSize = builder.MaxPoolSize;
                MinPoolSize = builder.MinPoolSize;
                MultipleActiveResultSets = builder.MultipleActiveResultSets;
                NetworkLibrary = builder.NetworkLibrary;
                PacketSize = builder.PacketSize;
                Password = builder.Password;
                PersistSecurityInfo = builder.PersistSecurityInfo;
                Pooling = builder.Pooling;
                Replication = builder.Replication;
                TransactionBinding = builder.TransactionBinding;
                TrustServerCertificate = builder.TrustServerCertificate;
                TypeSystemVersion = builder.TypeSystemVersion;
                UserID = builder.UserID;
                UserInstance = builder.UserInstance;
                WorkstationID = builder.WorkstationID;

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }
    }
}
