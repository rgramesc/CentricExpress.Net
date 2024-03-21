Steps towards persistent storage:
- install Dapper NuGet package from https://www.nuget.org/packages/Dapper as an ORM library
- install Microsoft.Data.SqlClient package from https://www.nuget.org/packages/Microsoft.Data.SqlClient for the data provider for SQL Server Express LocalDb database
- create connection and transaction in database context class
- use transaction for saving multiple data in the same transaction

References:
- https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/cqrs-microservice-reads#use-dapper-as-a-micro-orm-to-perform-queries
- https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
- https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/async-scenarios
- https://medium.com/@kdowswell/dapper-ef-and-cqs-2b044206af06#.akslp1fqn
- https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
- https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/june/the-unit-of-work-pattern-and-persistence-ignorance
- https://learn.microsoft.com/en-us/sql/t-sql/language-elements/transactions-transact-sql?view=sql-server-ver16


Possible issues:
- https://stackoverflow.com/questions/66080953/unable-to-connect-to-localdb-mssqllocaldb-due-to-trigger-execution