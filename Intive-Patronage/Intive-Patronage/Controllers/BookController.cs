using Intive_Patronage.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Intive_Patronage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        [HttpPost]
        public void AddBook(string Title, string Description, decimal Rating, string ISBN, DateTime PublicationDate)
        {
            Book book = new Book(Title, Description, Rating, ISBN, PublicationDate);
            if (_libraryDbContext.Database.CanConnect())
            {
                _libraryDbContext.Book.Add(book);
                _libraryDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Nieudalo sie polaczyc");
            }
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            if (_libraryDbContext.Book.Count() > 0)
            {
                return _libraryDbContext.Book;
            }
            else return Enumerable.Empty<Book>();
        }

        [HttpPut]
        public void UpdateBook(int Id, string newTitle)
        {
            var books = GetBooks();
 
            if (books.Any(b => b.Id == Id))
            {
                _libraryDbContext.Book.Find(Id).Title = newTitle;
                _libraryDbContext.SaveChanges();
            }
            else
            {
                Response.StatusCode = 404;
            }
        }
        [HttpDelete]
        public void DeleteBook(int Id)
        {
            var books = GetBooks();

            if (books.Any(b => b.Id == Id))
            {
                _libraryDbContext.Book.Where(b => b.Id == Id).ExecuteDelete();
                _libraryDbContext.SaveChanges();
            }
            else
            {
                Response.StatusCode = 404;
            }
        }

        [HttpGet]
        [Route("GetBookById")]
        public Object FindBook(int Id)
        {
           
            foreach (Book book in GetBooks())
            {
                if (book.Id == Id)
                {
                    return book;
                }
            }
                return NotFound();
        }
    }
}
