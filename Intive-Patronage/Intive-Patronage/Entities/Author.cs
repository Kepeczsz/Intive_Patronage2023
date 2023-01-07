using System.ComponentModel.DataAnnotations;

namespace Intive_Patronage.Entities
{
   public class Author
   {
      public int AuthorId { get; set; }

      [MaxLength(50)]
      [Required]
      public string FirstName { get; set; }

      [MaxLength(50)]
      [Required]
      public string LastName { get; set; }

      [Required]
      public DateTime BirthDate { get; set; }

      [Required]
      public bool Gender { get; set; }

      public ICollection<BookAuthor>? BookAuthors { get; set; }
   }
}
