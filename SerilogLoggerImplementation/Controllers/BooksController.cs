using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerilogLoggerImplementation.Models;

namespace SerilogLoggerImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> Books = new List<Book>()
        {
            new Book() { Id = 1001, Title = "ASP.NET Core", Author = "Pranaya", YearPublished = 2019 },
            new Book() { Id = 1002, Title = "SQL Server", Author = "Pranaya", YearPublished = 2022 }
        };

        // Inject the ILogger for BooksController to capture logs
        private readonly ILogger<BooksController> _logger;

        // Constructor injection of ILogger<T>
        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        // POST api/books
        // This action adds a new book to the list
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            Books.Add(book); // Add the book to our static list

            // Log the newly added book using structured logging.
            // The '@' operator tells Serilog to serialize the object as structured data.
            // {@Book} tells Serilog to capture all properties as structured data
            _logger.LogInformation("Added a new book {@Book}", book);
             
            return Ok();
        }

        // GET api/books
        // This action retrieves all books in the list
        [HttpGet]
        public IActionResult GetBooks()
        {
            // Log the retrieval of books and include the entire collection as structured data.
            // Logs the list of books as structured data
            _logger.LogInformation("Retrieved all books. Books: {@Books}", Books);

            return Ok(Books);
        }
    }
}
