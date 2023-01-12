using FluentValidation.Results;
using Intive_Patronage.Entities;
using Intive_Patronage.Validators;
using Microsoft.AspNetCore.Mvc;
using Models.DbSets;

namespace Intive_Patronage.Controllers
{
   [Route("[controller]")]
   [ApiController]
   public class AuthorController : ControllerBase
   {
      private readonly LibraryDbContext _libraryDbContext;
      private AuthorValidator validator = new AuthorValidator();

      public AuthorController(LibraryDbContext libraryDbContext)
      {
         _libraryDbContext = libraryDbContext;
      }

      /// <summary>
      /// Adds Author via postman using json format.
      /// </summary>
      /// <param name="author"></param>
      /// <returns></returns>
      [HttpPost]
      public ActionResult<Author> AddAuthor([FromBody] Author author)
      {
         ValidationResult results = validator.Validate(author);
         if (!results.IsValid)
            return BadRequest(results.Errors);

         _libraryDbContext.Author.Add(author);
         _libraryDbContext.SaveChanges();
         return Ok();
      }

      /// <summary>
      /// Returns All Authors from Database.
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public ActionResult<IEnumerable<Author>> GetAuthors()
      {
         var authors = _libraryDbContext.Author.ToList();
         return Ok(authors);
      }

      /// <summary>
      /// Returns all Authors with given name
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      [HttpGet]
      [Route("GetAuthorsByName")]
      public ActionResult<IEnumerable<Author>> GetAuthorsByName(string name)
      {
         var authors =
          from a in _libraryDbContext.Author
          where a.FirstName == name
          select a;
         if (authors.Any())
            return Ok(authors);
         return NotFound("There wasn't any author with that name");
      }
   }
}
