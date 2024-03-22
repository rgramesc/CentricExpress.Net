using CentricExpress.BooksLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryList.BooksLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private IList<Book> _books { get; set; }

        public BooksController()
        {
            var patternsBookAuthors = new Author[]
            {
                    new("Martin>", "Fowler")
            };

            _books = new List<Book>
            {
                new(patternsBookAuthors, "Patterns of Enterprise Application Architecture", "Addison-Wesley Professional")
            };
        }

        [HttpGet(Name = "Books")]
        public IList<Book> Get()
        {
            return _books;
        }

        [HttpPost(Name = "Books")]
        public IActionResult Post(Book book)
        {
            _books.Add(book);

            //add breakpoint, to see the list of _books with the newly added book(s)
            return Ok(_books);
        }
    }
}
