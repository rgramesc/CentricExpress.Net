namespace BooksLibrary.WebAPI.Contracts;

public class AuthorApiDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}