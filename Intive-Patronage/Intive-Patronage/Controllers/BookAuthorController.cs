using Intive_Patronage.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intive_Patronage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BookAuthorController(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        [HttpPost]
        public void AddBookToAuthor(int BookId, int AuthorId)
        {
            
            BookAuthor bookAuthor = new BookAuthor();
            bookAuthor.AuthorId = AuthorId;
            bookAuthor.BookId = BookId;
            foreach(Author author in _libraryDbContext.Author)
            {
                if(author.Id == AuthorId)
                {
                    bookAuthor.Author = author;
                }
            }
            foreach (Book book in _libraryDbContext.Book)
            {
                if (book.Id == BookId)
                {
                    bookAuthor.Book = book;
                }
            }

            if (_libraryDbContext.Database.CanConnect())
            {
                _libraryDbContext.BookAuthor.Add(bookAuthor);
                _libraryDbContext.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<BookAuthor> GetAllConnections()
        {
            return _libraryDbContext.BookAuthor;
        }

        [HttpGet]
        [Route("test")]
        public Object ReturnAuthorByBookId(int BookId)
        {
            var books = _libraryDbContext.BookAuthor;
            List<int> authors = new List<int>(); 
            foreach(BookAuthor bookAuthor in books)
            {
                if(bookAuthor.BookId == BookId)
                {
                    authors.Add(bookAuthor.AuthorId);
                }
            }
            if (authors.Count == 0)
            {
                return Response.StatusCode = 204;
            }
            return authors;
        }
    }
}
