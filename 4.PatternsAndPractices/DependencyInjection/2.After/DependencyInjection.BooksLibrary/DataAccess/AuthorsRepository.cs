using CentricExpress.BooksLibrary.Models;

namespace InMemoryRepository.BooksLibrary.DataAccess
{
    public class AuthorsRepository
    {
        private IList<Author> _authors { get; set; }

        public AuthorsRepository()
        {
            _authors = new List<Author>();
        }

        public void AddAuthor(Author author)
        {
            if (_authors.Contains(author))
                return;

            _authors.Add(author);
        }

        public IList<Author> GetAuthors()
        {
            return _authors;
        }
    }
}
