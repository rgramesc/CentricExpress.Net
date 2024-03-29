using Microsoft.AspNetCore.Mvc;

namespace PracticeApp.Controllers
{
    [ApiController]
    [Route("api/books/[controller]")]
    public class BookLibraryController : ControllerBase
    {
        private static List<Book> _books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Neuroplasticitatea",
                Author = "Leon Danaila",
                Year = 2024,
                Publisher = "Nemira",
                Genre = "Biography",
                Stock = 25
            },
            new Book
            {
                Id = 2,
                Title = "Elon Musk",
                Author = "Walter Isaacson",
                Year = 2023,
                Publisher = "Publica",
                Genre = "Comedy",
                Stock = 90
            },
            new Book
            {
                Id = 3,
                Title = "Tatuatorul de la Auschwitz",
                Author = "Heather Morris",
                Year = 2022,
                Publisher = "Publica",
                Genre = "Moderni",
                Stock = 1
            },
            new Book
            {
                Id = 4,
                Title = "Trei surori",
                Author = "Heather Morris",
                Year = 207,
                Publisher = "Humanitas",
                Genre = "Fiction",
                Stock = 13
            }
        };

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_books);
        }

        [HttpGet("getByQuery")]
        public IActionResult GetBooksByQuery([FromQuery] BookQuery bookQuery)
        {
            var books = _books.Find(x => x.Id == bookQuery.Id || x.Author == bookQuery.Author);

            return Ok(books);
        }

        [HttpGet("getById/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var book = _books.Find(x => x.Id == id);

            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] Book book)
        {
            _books.Add(book);

            return Created();
        }

        [HttpPut("update")]
        public IActionResult Put([FromBody] Book updatedBook)
        {
            var book = _books.Find(x => x.Id == updatedBook.Id);
            if (book == null)
            {
                _books.Add(updatedBook);

                return NoContent();
            }

            _books.Where(x => x.Id == updatedBook.Id).ToList().ForEach(x =>
            {
                x.Author = updatedBook.Author;
                x.Title = updatedBook.Title;
                x.Year = updatedBook.Year;
                x.Publisher = updatedBook.Publisher;
                x.Genre = updatedBook.Genre;
                x.Stock = updatedBook.Stock;
            });

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var book = _books.Find(x => x.Id == id);

            _books.Remove(book);

            return NoContent();
        }

        [HttpGet("getByAuthor/{author}")]
        public IActionResult GetByAuthor([FromRoute] string author)
        {
            var book = _books.Find(x => x.Author == author);

            return book == null ? NotFound() : Ok(book);
        }

        [HttpDelete("deleteBooks/{year}")]
        public IActionResult DeleteBooks([FromRoute] int year)
        {
            _books.Where(x => x.Year <= year).ToList().ForEach(x =>
            {
                _books.Remove(x);
            });

            return NoContent();
        }

        [HttpPut("updateStock")]
        public IActionResult UpdateStock([FromQuery] ExerciseQuery query)
        {
            _books.Where(x => x.Author == query.Author && x.Stock < query.MinValue).ToList().ForEach(x =>
            {
                x.Stock = 50;
            });

            return NoContent();
        }
    }
}
