SQL Server MS Build Tasks
=========================

A couple of extra MSBuild tasks for SQL Server scripting.

### Examples

Dropping a database:

    <SqlDropDatabase ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;"
                     Database="AdventureWorks" />

Creating an empty database:
    
    <SqlCreateDatabase ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;"
                       Database="AdventureWorks" />
    
Parsing individual keys out of a SQL connection string:

    <SqlParseConnectionString ConnectionString="Server=.\SQLEXPRESS;Database=AdventureWorks;Integrated Security=SSPI;">
        <Output PropertyName="myDb" TaskParameter="InitialCatalog" />
        <Output PropertyName="myServer" TaskParameter="DataSource" />
        <Output PropertyName="myTimeout" TaskParameter="ConnectTimeout" />
    </SqlParseConnectionString>
    <Message Text="Parsed the $(myDb) database on server $(myServer) with timeout = $(myTimeout)." />


### License

Available under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).