namespace BooksLibrary.Domain;

public class Book(Guid id, List<Author> authors, DateOnly publicationDate, string title, bool isRemoved)
{
    public Guid Id { get; private set; } = id;

    public List<Author> Authors { get; private set; } = authors;

    public DateOnly PublicationDate { get; private set; } = publicationDate;

    public string Title { get; private set; } = title;

    public bool IsRemoved { get; private set; } = isRemoved;

    public static Result<Book?> CreateNewBook(List<Author> authors, DateOnly publicationDate, string title, List<string> existingBookTitles)
    {
        if (existingBookTitles.Find(existingTitle => existingTitle == title) != null)
        {
            return Result<Book>.Failure($"Book with title '{title}' already exists.");
        }

        var book = new Book(Guid.NewGuid(), authors, publicationDate, title, false);
        return Result<Book?>.Success(book);
    }

    public Result MarkAsRemoved()
    {
        if (IsRemoved)
        {
            return Result.Failure("The book is already marked as removed");
        }

        IsRemoved = true;

        return Result.Success();
    }
}