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
        public void AddAuthor(string firstName, string lastName)
        {
            Author author = new Author(firstName, lastName);
            if (_libraryDbContext.Database.CanConnect())
            {
                _libraryDbContext.Author.Add(author);
                _libraryDbContext.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<Author> GetAuthors()
        {
            return _libraryDbContext.Author;
        }

        [HttpGet]
        [Route("GetAuthorsByName")]
        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            var authors = new List<Author>();
            foreach (Author author in GetAuthors())
            {
                if (author.FirstName == name)
                {
                    authors.Add(author);
                }
            }
            return authors;
        }
    }
}
