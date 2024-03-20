using Dapper;

namespace BooksLibrary;

public class BooksLibraryDatabaseCreation
{
    private readonly BooksDatabaseTransactionalContext _booksDatabaseContext;
    private readonly BookUnitOfWork _bookUnitOfWork;

    public BooksLibraryDatabaseCreation(
        BooksDatabaseTransactionalContext booksDatabaseContext,
        BookUnitOfWork bookUnitOfWork)
    {
        _booksDatabaseContext = booksDatabaseContext;
        _bookUnitOfWork = bookUnitOfWork;
    }

    public async Task InitiateDatabaseTablesWithData(bool shouldDeleteTables)
    {
        if (shouldDeleteTables)
        {
            await DeleteTables();
        }

        await _booksDatabaseContext.OpenDatabaseConnection();
        await CreateBooksTable();
        await CreateAuthorsTable();
        await CreateBookAuthorTable();
        await SeedDataInDatabase();
    }

    private async Task CreateBooksTable()
    {
        const string checkIfBooksTableExistsSql = "SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Books'";
        var connection = _booksDatabaseContext.DatabaseConnection;
        var booksTableCount = await connection.ExecuteScalarAsync<int>(checkIfBooksTableExistsSql);
        if (booksTableCount == 0)
        {
            var createBooksTableSql = @"
CREATE TABLE Books (
Id UNIQUEIDENTIFIER CONSTRAINT ""PK_Books"" PRIMARY KEY,
PublicationDate DATE NOT NULL,
Title NVARCHAR(150) NOT NULL)";
            await connection.ExecuteAsync(createBooksTableSql);
        }
    }

    private async Task CreateAuthorsTable()
    {
        const string checkIfAuthorsTableExistsSql = "SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Authors'";
        var connection = _booksDatabaseContext.DatabaseConnection;
        var authorsTableCount = await connection.ExecuteScalarAsync<int>(checkIfAuthorsTableExistsSql);
        if (authorsTableCount == 0)
        {
            var createBooksTableSql = @"
CREATE TABLE Authors (
Id UNIQUEIDENTIFIER CONSTRAINT ""PK_Authors"" PRIMARY KEY,
FirstName NVARCHAR(150) NOT NULL,
LastName NVARCHAR(150) NOT NULL)";
            await connection.ExecuteAsync(createBooksTableSql);
        }
    }

    private async Task CreateBookAuthorTable()
    {
        const string checkIfBookAuthorTableExistsSql = "SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BookAuthor'";
        var connection = _booksDatabaseContext.DatabaseConnection;
        var bookAuthorTableCount = await connection.ExecuteScalarAsync<int>(checkIfBookAuthorTableExistsSql);
        if (bookAuthorTableCount == 0)
        {
            var createBooksTableSql = @"
CREATE TABLE BookAuthor (
BookId UNIQUEIDENTIFIER,
AuthorId UNIQUEIDENTIFIER,
CONSTRAINT ""PK_BookAuthor"" PRIMARY KEY(BookId, AuthorId),
CONSTRAINT ""FK_BookAuthor_Books_BookId"" FOREIGN KEY (""BookId"") REFERENCES ""Books"" (""Id"") ON DELETE CASCADE,
CONSTRAINT ""FK_BookAuthor_Books_AuthorId"" FOREIGN KEY (""AuthorId"") REFERENCES ""Authors"" (""Id"") ON DELETE CASCADE)";
            await connection.ExecuteAsync(createBooksTableSql);
        }
    }

    private async Task SeedDataInDatabase()
    {
        const string checkIfBooksExistsSql = "SELECT COUNT(ID) FROM BOOKS";
        var connection = _booksDatabaseContext.DatabaseConnection;
        var booksCount = await connection.ExecuteScalarAsync<int>(checkIfBooksExistsSql);
        if (booksCount > 0)
        {
            return;
        }

        var patternsBook = new Book(new Author("Erich", "Gamma"), new DateOnly(1994, 1, 1),
            "Design Patterns: Elements of Reusable Object-Oriented Software");
        await _bookUnitOfWork.AddBook(patternsBook);

        var cloudDataPatternsBook = new Book(new Author("Rob", "Vettor"), new DateOnly(2024, 1, 2),
            "Architecting Cloud Native .NET Applications for Azure");
        await _bookUnitOfWork.AddBook(cloudDataPatternsBook);
    }
        
    private async Task DeleteTables()
    {
        const string dropTablesSql = "Drop TABLE BookAuthor;\r\nDrop TABLE Authors;\r\nDrop TABLE Books;";
        var connection = _booksDatabaseContext.DatabaseConnection;
        await connection.ExecuteAsync(dropTablesSql);
    }
}