using Domain;

namespace Application.Interfaces
{
    public interface IBookUnitOfWork
    {
        public Task AddBook(Book book);
    }
}
