using FluentValidation.Results;
using Intive_Patronage.Entities;
using Intive_Patronage.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DbSets;
using System.Text.RegularExpressions;

namespace Intive_Patronage.Controllers
{
   [Route("[controller]")]
   [ApiController]
   public class BookController : ControllerBase
   {
      private readonly LibraryDbContext _libraryDbContext;
      private BookValidator  validator = new BookValidator();

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

         book.Title = newTitle;
         ValidationResult results = validator.Validate(book);
         if (results.IsValid) {
            _libraryDbContext.SaveChanges();
            return Ok();
         }
         return BadRequest(results.Errors);
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
      public ActionResult<Book> AddBook(string AuthorId, [FromBody] Book book)
      {
         ValidationResult results = validator.Validate(book);
         if (!results.IsValid)
            return BadRequest(results.Errors.ToString());

         if (!Regex.IsMatch(AuthorId, @"^\d+(,\d+)*$"))
            return BadRequest("AuthorId can not contain letters!");
         // Split the list of author IDs into an array
         int[] authorIds = AuthorId.Split(',').Select(int.Parse).ToArray();
         // Retrieve the authors from the database
         var authors = _libraryDbContext.Author.Where(a => authorIds.Contains(a.AuthorId)).ToList();
         if(!authors.Any())
            return BadRequest("There was not any authors with given id");
         book.BookAuthors = authors.Select(a => new BookAuthor { Author = a }).ToList();
         // Add the book and book-author relationships to the database
         _libraryDbContext.Book.Add(book);
         _libraryDbContext.BookAuthor.AddRange(book.BookAuthors);
         _libraryDbContext.SaveChanges();
         // Return the created book
         return Created($"/Book/{book.BookId}", null);
      }
   }
}
