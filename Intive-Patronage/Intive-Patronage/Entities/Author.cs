using System.ComponentModel.DataAnnotations; 
namespace Intive_Patronage.Entities
{
   public class Author
   {
      public int AuthorId { get; set; }

      [MaxLength(50)]
      [Required]
      [RegularExpression("^[A-Z][a-z]+",
         ErrorMessage = "your name need to start with Capital Letter, can not contain numbers" +
         " and can be no longer than 50 characters")]
      public string FirstName { get; set; }

      [MaxLength(50)]
      [Required]
      [RegularExpression("^[A-Z][a-z]+",
         ErrorMessage = "your Surname need to start with Capital Letter, can not contain numbers" +
         " and can be no longer than 50 characters")]
      public string LastName { get; set; }

      [Required]
      public DateTime BirthDate { get; set; }

      [Required]
      public bool Gender { get; set; }

      public ICollection<BookAuthor>? BookAuthors { get; set; }
   }
}
