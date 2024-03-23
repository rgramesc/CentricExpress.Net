using BooksLibrary.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BooksLibrary.WebAPI.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<BooksController>
{

    public CustomWebApplicationFactory()
    {
        Client = CreateClient();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(configurationBuilder =>
        {
            var databaseName = "BooksLibraryWithDeleteDataTests";
            configurationBuilder.AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    ["DataAccess:DatabaseName"] = databaseName,
                    ["DataAccess:DataDirectoryRelativePath"] = "..\\..\\..\\AppData",
                    ["DataAccess:BooksLibraryDatabaseConnection"] =
                        $"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog={databaseName};Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\\{databaseName}.mdf",
                });
        });

        return base.CreateHost(builder);
    }

    public HttpClient Client { get; private set; }
}