using System.Net;
using System.Net.Http.Json;
using BooksLibrary.Infrastructure;
using BooksLibrary.WebAPI.Contracts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BooksLibrary.WebAPI.IntegrationTests
{
    public class BooksControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory _factory;

        public BooksControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_ReturnData_When_GetBooks()
        {
            // Arrange
            await SeedData();
            var url = "books";

            // Act
            var response = await _factory.Client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var books = await response.Content.ReadFromJsonAsync<List<BookApiDto>>();
            books.Should().HaveCount(1);
        }

        [Fact]
        public async Task Post_EndpointWithExistingTitleReturnBadRequestWithErrorMessage()
        {
            // Arrange
            await SeedData();
            var url = "books";
            var bookApiDto = new BookApiDto
            {
                Authors = new List<AuthorApiDto>
                {
                    new()
                    {
                        FirstName = "Rob",
                        LastName = "Vettor"
                    },
                },
                PublicationDate = new DateOnly(2024, 1, 2).ToDateTime(TimeOnly.MinValue),
                Title = "Architecting Cloud Native .NET Applications for Azure"
            };

            // Act
            var response = await _factory.Client.PostAsJsonAsync(url, bookApiDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadAsStringAsync();
            error.Should().Be($"Book with title '{bookApiDto.Title}' already exists.");
        }

        [Fact]
        public async Task Post_EndpointWithNonExistingTitleReturnSuccessWithNewBook()
        {
            // Arrange
            var url = "books";
            var bookApiDto = new BookApiDto
            {
                Authors = new List<AuthorApiDto>
                {
                    new()
                    {
                        FirstName = "Erich",
                        LastName = "Gamma"
                    },
                    new()
                    {
                        FirstName = "Richard",
                        LastName = "Heml"
                    },
                    new()
                    {
                        FirstName = "Ralph",
                        LastName = "Johnson"
                    },
                    new()
                    {
                        FirstName = "John",
                        LastName = "Vlissides"
                    }
                },
                PublicationDate = DateTime.Now.Date,
                Title = "Refactoring: Improving the Design of Existing Code (2nd Edition) (Addison-Wesley Signature Series (Fowler))"
            };

            // Act
            var response = await _factory.Client.PostAsJsonAsync(url, bookApiDto);

            // Assert
            response.EnsureSuccessStatusCode();
            var newlyAddedBook = await response.Content.ReadFromJsonAsync<BookApiDto>();
            newlyAddedBook.Should().NotBeNull();
            newlyAddedBook.Title.Should().Be(bookApiDto.Title);
        }

        public async Task InitializeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var booksLibraryDatabaseCreation = scope.ServiceProvider.GetRequiredService<BooksLibraryDatabaseCreation>();
            await booksLibraryDatabaseCreation.DeleteDataInTables();
        }

        public async Task DisposeAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var booksLibraryDatabaseCreation = scope.ServiceProvider.GetRequiredService<BooksLibraryDatabaseCreation>();
            await booksLibraryDatabaseCreation.DeleteDataInTables();
        }

        private async Task SeedData()
        {
            using var scope = _factory.Services.CreateScope();
            var booksLibraryDatabaseCreation = scope.ServiceProvider.GetRequiredService<BooksLibraryDatabaseCreation>();
            await booksLibraryDatabaseCreation.SeedDataInDatabase();
        }
    }
}