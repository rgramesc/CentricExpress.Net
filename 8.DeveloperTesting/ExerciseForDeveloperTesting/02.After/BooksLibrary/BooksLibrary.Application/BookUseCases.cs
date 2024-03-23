using BooksLibrary.Application.InfrastructureInterfaces;
using BooksLibrary.Domain;
using BooksLibrary.WebAPI.Contracts;
using System.Linq;

namespace BooksLibrary.Application
{
    public class BookUseCases
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookUnitOfWork _bookUnitOfWork;
        private readonly IBookRepository _bookRepository;

        public BookUseCases(IBookAuthorRepository bookAuthorRepository, IBookUnitOfWork bookUnitOfWork, IBookRepository bookRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _bookUnitOfWork = bookUnitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<List<BookApiDto>> GetBooks()
        {
            var booksWithAuthors = await _bookAuthorRepository.GetBooksWithAuthors();
            var books = new List<BookApiDto>();
            booksWithAuthors.GroupBy(bookWithAuthor => bookWithAuthor.BookId).ToList().ForEach(bookGroup =>
            {
                var booksInGroup = bookGroup.ToList();
                var firstBookInGroup = booksInGroup.First();
                var authors = new List<AuthorApiDto>();
                booksInGroup.ForEach(bookWithAuthor => authors.Add(new AuthorApiDto
                {
                    Id = bookWithAuthor.AuthorId,
                    FirstName = bookWithAuthor.AuthorFirstName,
                    LastName = bookWithAuthor.AuthorLastName
                }));
                books.Add(new BookApiDto
                {
                    Id = firstBookInGroup.BookId,
                    Authors = authors,
                    PublicationDate = firstBookInGroup.BookPublicationDate,
                    Title = firstBookInGroup.BookTitle
                });
            });
            return books;
        }

        public async Task<Result<BookApiDto?>> AddBook(NewBookApiDto newBookDto)
        {
            var existingBookTitles = await _bookRepository.GetBookTitles();

            var authors = new List<Author>();
            newBookDto.Authors.ForEach(author => authors.Add(new Author(author.FirstName, author.LastName)));
            var createBookResult = Book.CreateNewBook(authors, DateOnly.FromDateTime(newBookDto.PublicationDate), newBookDto.Title, existingBookTitles);
            if (createBookResult.IsFailure)
            {
                return Result<BookApiDto?>.Failure(createBookResult.Error);
            }

            await _bookUnitOfWork.AddBook(createBookResult.Value);

            var appiDtoAuthors = new List<AuthorApiDto>();
            createBookResult.Value.Authors.ForEach(author => appiDtoAuthors.Add(new AuthorApiDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            }));
            var bookApiDto = new BookApiDto
            {
                Id = createBookResult.Value.Id,
                Authors = appiDtoAuthors,
                PublicationDate = createBookResult.Value.PublicationDate.ToDateTime(TimeOnly.MinValue),
                Title = createBookResult.Value.Title
            };
            return Result<BookApiDto?>.Success(bookApiDto);
        }

        public async Task DeleteBook(Guid bookId)
        {
            await _bookRepository.DeleteBook(bookId);
        }

        public async Task<Result> MarkBookAsRemoved(Guid bookId)
        {
            var bookWithAuthors = await _bookAuthorRepository.GetBookWithAuthors(bookId);
            if (bookWithAuthors == null)
            {
                return Result.Failure("The book does not exist.");
            }

            var firstBookInGroup = bookWithAuthors.GroupBy(bookWithAuthor => bookWithAuthor.BookId).ToList().First();
            var booksInGroup = firstBookInGroup.ToList();
            var bookDto = booksInGroup.First();
            var authors = new List<Author>();
            booksInGroup.ForEach(bookWithAuthor => authors.Add(new Author(bookWithAuthor.AuthorId, bookWithAuthor.AuthorFirstName, bookWithAuthor.AuthorLastName)));
            var book = new Book(bookDto.BookId, authors, DateOnly.FromDateTime(bookDto.BookPublicationDate), bookDto.BookTitle, bookDto.BookIsRemoved);
            var result = book.MarkAsRemoved();
            if (result.IsFailure)
            {
                return result;
            }

            await _bookRepository.MarkBookAsRemoved(book);

            return Result.Success();
        }
    }
}
