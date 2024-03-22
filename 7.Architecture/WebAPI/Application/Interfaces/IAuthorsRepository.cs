using Domain;

namespace Application.Interfaces
{
    public interface IAuthorsRepository
    {
        public Task<List<Book>> GetBooks();
        public Task AddAuthor(Author author);
    }
}
