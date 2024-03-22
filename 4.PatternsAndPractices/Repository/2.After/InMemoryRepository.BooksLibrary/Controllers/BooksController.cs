using CentricExpress.BooksLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryRepository.BooksLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private BooksRepository _repository { get; set; }

        public BooksController()
        {
           _repository = new BooksRepository();
        }

        [HttpGet(Name = "books")]
        public IList<Book> Get()
        {
            return _repository.GetAllBooks();
        }

        [HttpPost(Name = "Books")]
        public IActionResult Post(Book book)
        {
            _repository.AddBook(book);

            return Ok(book);
        }
    }
}
