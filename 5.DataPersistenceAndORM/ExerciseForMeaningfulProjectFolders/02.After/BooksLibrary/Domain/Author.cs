namespace BooksLibrary.Domain;

public class Author(Guid id, string firstName, string lastName)
{
    public Author(string firstName, string lastName) : this(Guid.NewGuid(), firstName, lastName)
    {
    }

    public Guid Id { get; private set; } = id;

    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;
}