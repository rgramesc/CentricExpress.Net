using Domain;
using InfrastructureContracts;

namespace Application.Interfaces
{
    public interface IBookAuthorRepository
    {
        public Task<List<BookWithAuthorDataDto>> GetBooksWithAuthors();
        public Task AddBookAuthor(Book book);
    }
}
