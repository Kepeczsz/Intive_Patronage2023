using System.ComponentModel.DataAnnotations;
namespace Intive_Patronage.Entities
{
   public class Author
   {
      public int AuthorId { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public DateTime BirthDate { get; set; }
      public bool Gender { get; set; }
      public ICollection<BookAuthor>? BookAuthors { get; set; }
   }
}
