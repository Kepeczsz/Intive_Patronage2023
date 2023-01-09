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
      [HttpGet]
      public ActionResult<IEnumerable<Book>> GetBooks()
      {
         var books = _libraryDbContext.Book;
         return Ok(books);
      }
      [HttpPut]
      public ActionResult<Book> UpdateBook(int Id, string newTitle)
      {
         var book = _libraryDbContext.Book.FirstOrDefault(b => b.BookId == Id);
         if (book is null)
         {
            return NotFound();
         }
         if (ModelState.IsValid)
         {
            book.Title = newTitle;
            _libraryDbContext.SaveChanges();
            return Ok();
         }
         return BadRequest();
      }
      [HttpDelete("{id}")]
      public ActionResult<Book> DeleteBook([FromRoute] int id)
      {
         var book = _libraryDbContext.Book.Include(e => e.BookAuthors).FirstOrDefault(b => b.BookId == id);
         if (book is null)
         {
            return BadRequest($"Book with {id} not found");
         }
         _libraryDbContext.Book.Remove(book);
         _libraryDbContext.SaveChanges();
         return Ok();
      }
      [HttpGet("{id}")]
      public ActionResult<Book> FindBook([FromRoute] int id)
      {
         var book = _libraryDbContext.Book.FirstOrDefault(b => b.BookId == id);
         if (book is null)
         {
            return NotFound($"Book with id {id} not found");
         }
         return Ok(book);
      }
      [HttpPost]
      public ActionResult<Book> AddBook(int AuthorId, [FromBody] Book book)
      {
         var author = _libraryDbContext.Author.Find(AuthorId);
         if (author is null)
         {
            return BadRequest();
         }
         book.BookAuthors = new List<BookAuthor>
    {
        new BookAuthor
        {
            Author = author
        }
    };
         var bookAuthor = new BookAuthor
         {
            Author = author,
            Book = book,
         };
         if (ModelState.IsValid)
         {
            _libraryDbContext.Book.Add(book);
            _libraryDbContext.BookAuthor.Add(bookAuthor);
            _libraryDbContext.SaveChanges();
            return Created($"/Book/{book.BookId}", null);
         }
         return BadRequest();
      }
   }
}
