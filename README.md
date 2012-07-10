SQL Server MS Build Tasks
=========================

A couple of handy MSBuild tasks for SQL Server scripting. Works with .NET 2.0+.

### Examples

Dropping a database:
```xml
<SqlDropDatabase ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;"
                 Database="AdventureWorks" />
```
Creating an empty database:
```xml
<SqlCreateDatabase ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;"
                   Database="AdventureWorks" />
```
Parsing individual keys out of a SQL connection string:
```xml
<SqlParseConnectionString ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;">
    <Output PropertyName="myDb" TaskParameter="InitialCatalog" />
    <Output PropertyName="myServer" TaskParameter="DataSource" />
    <Output PropertyName="myTimeout" TaskParameter="ConnectTimeout" />
</SqlParseConnectionString>
<Message Text="Parsed the $(myDb) database on server $(myServer) with timeout = $(myTimeout)." />
```

### License

Available under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).