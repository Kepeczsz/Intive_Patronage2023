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
      private BookValidator validator = new BookValidator();

      public BookController(LibraryDbContext libraryDbContext)
      {
         _libraryDbContext = libraryDbContext;
      }

      /// <summary>
      /// Returns All Books from Database.
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public ActionResult<IEnumerable<Book>> GetBooks()
      {
         var books = _libraryDbContext.Book;
         return Ok(books);
      }

      /// <summary>
      /// Changes title of book with given Id.
      /// </summary>
      /// <param name="Id"></param>
      /// <param name="newTitle"></param>
      /// <returns></returns>
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
         if (results.IsValid)
         {
            _libraryDbContext.SaveChanges();
            return Ok();
         }
         return BadRequest(results.Errors);
      }

      /// <summary>
      /// Deletes book with given id.
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id}")]
      public ActionResult<Book> DeleteBook([FromRoute] int id)
      {
         var book = _libraryDbContext.Book.Include(e => e.BookAuthors).FirstOrDefault(b => b.BookId == id);
         if (book is null)
         {
            return BadRequest($"This book doesn't exist");
         }
         _libraryDbContext.Book.Remove(book);
         _libraryDbContext.SaveChanges();
         return Ok();
      }

      /// <summary>
      /// Returs Book with given id
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
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

      /// <summary>
      /// Adds Book via postman using json format, AuthorId is a string of authors id, seperated only by "," character. 
      /// </summary>
      /// <param name="AuthorId"></param>
      /// <param name="book"></param>
      /// <returns></returns>
      [HttpPost]
      public ActionResult<Book> AddBook(string AuthorId, [FromBody] Book book)
      {
         ValidationResult results = validator.Validate(book);
         if (!results.IsValid)
            return BadRequest(results.Errors);

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
