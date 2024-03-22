namespace CentricExpress.BooksLibrary.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Author(string firstName, string lastName)
        {
            Id = Guid.NewGuid();

            FirstName = firstName;
            LastName = lastName;
        }

        public string GetAuthorName()
        {
            return $"{LastName}, {FirstName}";
        }
    }
}
