using Intive_Patronage.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Intive_Patronage.Controllers
{
   [Route("[controller]")]
   [ApiController]
   public class AuthorController : ControllerBase
   {
      private readonly LibraryDbContext _libraryDbContext;
      public AuthorController(LibraryDbContext libraryDbContext)
      {
         _libraryDbContext = libraryDbContext;
      }

      [HttpPost]
      public ActionResult<Author> AddAuthor([FromBody] Author author)
      {
         if (_libraryDbContext.Database.CanConnect())
         {
            _libraryDbContext.Author.Add(author);
            _libraryDbContext.SaveChanges();
            return Ok();
         }
         return BadRequest();
      }

      [HttpGet]
      public ActionResult<IEnumerable<Author>> GetAuthors()
      {
         var authors = _libraryDbContext.Author.ToList();
         return Ok(authors);
      }

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
