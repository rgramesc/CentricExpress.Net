using Domain;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        public Task AddBook(Book book);
    }
}
