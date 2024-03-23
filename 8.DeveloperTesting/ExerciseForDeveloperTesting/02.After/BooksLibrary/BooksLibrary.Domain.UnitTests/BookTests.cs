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

    [Fact]
    public void Should_ReturnSuccess_When_CreateBookWithNonExistingTitle()
    {
        // Arrange
        var authors = new List<Author>
        {
            new("Rob", "Vettor")
        };
        var existingBookTitle = "Working Effectively with Legacy Code";
        var bookTitle = "Architecting Cloud Native .NET Applications for Azure";
        var existingTitles = new List<string> { existingBookTitle };

        // Act
        var result = Book.CreateNewBook(authors, new DateOnly(2024, 1, 2), "Architecting Cloud Native .NET Applications for Azure", existingTitles);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Title.Should().Be(bookTitle);
    }

    [Fact]
    public void Should_ReturnFailure_When_MarkBookAsRemovedWithAlreadyMarked()
    {
        // Arrange
        var authors = new List<Author>
        {
            new("Rob", "Vettor")
        };
        var book = new Book(Guid.NewGuid(), authors, new DateOnly(2024, 1, 2), "Architecting Cloud Native .NET Applications for Azure", true);
        

        // Act
        var result = book.MarkAsRemoved();

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("The book is already marked as removed.");
    }

    [Fact]
    public void Should_ReturnSuccess_When_MarkBookAsRemoved()
    {
        // Arrange
        var authors = new List<Author>
        {
            new("Rob", "Vettor")
        };
        var book = new Book(Guid.NewGuid(), authors, new DateOnly(2024, 1, 2), "Architecting Cloud Native .NET Applications for Azure", false);

        // Act
        var result = book.MarkAsRemoved();

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}