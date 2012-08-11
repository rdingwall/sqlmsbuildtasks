SQL Server MS Build Tasks
=========================

A couple of handy MSBuild tasks for SQL Server scripting. Works with .NET 2.0+. Available under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).

### [>>> Get SqlMsBuildTasks via NuGet](http://nuget.org/List/Packages/sqlmsbuildtasks)

The latest SqlMsBuildTasks is **now available in NuGet [here](http://nuget.org/List/Packages/sqlmsbuildtasks)**, or as a zip from the downloads page.

Contributions and new ideas are welcome!

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

* ``DataSource`` (aka ``server``, ``address``, ``addr``, ``network address``)
* ``InitialCatalog`` (aka ``database``)
* ``ApplicationName`` (aka ``app``)
* ``AsynchronousProcessing`` 
* ``AttachDBFilename`` 
* ``ConnectTimeout`` (aka ``timeout``)
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
* ``Password`` (aka ``pwd``)
* ``PersistSecurityInfo`` 
* ``Pooling`` 
* ``Replication`` 
* ``TransactionBinding`` 
* ``TrustServerCertificate`` 
* ``TypeSystemVersion`` 
* ``UserID`` (aka ``User ID``, ``uid``, ``user``)
* ``UserInstance`` 
* ``WorkstationID``

See [MSDN](http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnectionstringbuilder.aspx) for a detailed description of the purpose of each of these keys.

# Using the Tasks
To use the tasks below, you will need to include a reference to the SqlMsBuildTasks.targets file in your MSBuild script:

```xml
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="path\to\SqlMsBuildTasks.targets" />

  <Target ...>
    <SqlCreateDatabase ... />
  </Target>
</Project>
```

...alternatively, you can simply reference the individual tasks directly:

```xml
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <UsingTask AssemblyFile="path\to\SqlMsBuildTasks.dll" TaskName="SqlMsBuildTasks.SqlCreateDatabase" />

  <Target ...>
    <SqlCreateDatabase ... />
  </Target>
</Project>
```

# Release History / Changelog
1.0.0.0 - August 11 2012

* Initial release on NuGet.