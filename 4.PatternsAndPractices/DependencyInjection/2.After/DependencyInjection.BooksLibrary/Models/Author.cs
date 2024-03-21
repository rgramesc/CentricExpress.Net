namespace CentricExpress.BooksLibrary.Models
{
    public class Author
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Author(string firstName, string lastName)
        {            
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetAuthorName()
        {
            return $"{LastName}, {FirstName}";
        }
    }
}
