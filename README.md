SQL Server MS Build Tasks
=========================

A couple of handy MSBuild tasks for SQL Server scripting. Works with .NET 2.0+. Available under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).

## SqlDropDatabase
MSBuild task to drop a SQL Server database.

#### Usage Example:
To drop the *AdventureWorks* database in a local SQL Server Express instance (using Windows Authentication):
```xml
<SqlDropDatabase ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;"
                 Database="AdventureWorks" />
```

#### Task Parameters:
* ``Database``: name of the database to be dropped.
* ``ConnectionString``: the connection string to use. Note any InitialCatalog will be ignored -- this task always uses the ``master`` database.

## SqlCreateDatabase
MSBuild task to create a SQL Server database.

#### Usage Example:
To create an empty database called *AdventureWorks* in the local SQL Server Express instance (using Windows Authentication):
```xml
<SqlCreateDatabase ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;"
                   Database="AdventureWorks" />
```

#### Task Parameters:
* ``Database``: name of the database to be created.
* ``ConnectionString``: the connection string to use. Note any InitialCatalog will be ignored -- this task always uses the ``master`` database.

## SqlParseConnectionString
MSBuild task to parse individual keys (e.g. server or port) out of a SQL Server connection string into separate properties. Parsed values can be read via [MSBuild Output Paramters](http://msdn.microsoft.com/en-us/library/ms164287.aspx).

#### Usage Example:
Parsing the server and database name out of a SQL connection string:
```xml
<SqlParseConnectionString ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;">
    <Output PropertyName="myDb" TaskParameter="InitialCatalog" />
    <Output PropertyName="myServer" TaskParameter="DataSource" />
</SqlParseConnectionString>
<Message Text="Parsed the $(myDb) database on server $(myServer)" />
```

#### Task Parameters:

* ``ConnectionString``: the connection string to parse.

#### Task Output Paramters:
The task uses [SqlConnectionStringBuilder](http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnectionstringbuilder.aspx) internally and can parse any key it supports:

* ``DataSource`` 
* ``InitialCatalog`` 
* ``ApplicationName`` 
* ``AsynchronousProcessing`` 
* ``AttachDBFilename`` 
* ``ConnectTimeout`` 
* ``ContextConnection`` 
* ``CurrentLanguage`` 
* ``Encrypt`` 
* ``Enlist`` 
* ``FailoverPartner`` 
* ``IntegratedSecurity`` 
* ``LoadBalanceTimeout`` 
* ``MaxPoolSize`` 
* ``MinPoolSize`` 
* ``MultipleActiveResultSets`` 
* ``NetworkLibrary`` 
* ``PacketSize`` 
* ``Password`` 
* ``PersistSecurityInfo`` 
* ``Pooling`` 
* ``Replication`` 
* ``TransactionBinding`` 
* ``TrustServerCertificate`` 
* ``TypeSystemVersion`` 
* ``UserID`` 
* ``UserInstance`` 
* ``WorkstationID``

See [MSDN](http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnectionstringbuilder.aspx) for a detailed description of the purpose of each of these keys.