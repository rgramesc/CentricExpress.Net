using FluentAssertions;

namespace BooksLibrary.Domain.UnitTests;

public class BookTests
{
    [Fact]
    public void Should_ReturnFailure_When_CreateBookWithExistingTitle()
    {
        // Arrange
        var authors = new List<Author>
        {
            new("Rob", "Vettor")
        };
        var bookTitle = "Architecting Cloud Native .NET Applications for Azure";
        var existingTitles = new List<string> {bookTitle};

        // Act
        var result = Book.CreateNewBook(authors, new DateOnly(2024, 1, 2), "Architecting Cloud Native .NET Applications for Azure", existingTitles);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Book with title '{bookTitle}' already exists.");
    }
}