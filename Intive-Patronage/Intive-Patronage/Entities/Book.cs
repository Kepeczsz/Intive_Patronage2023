using System.ComponentModel.DataAnnotations;

namespace Intive_Patronage.Entities
{
   public class Book
   {
      public int BookId { get; set; }

      [MaxLength(100)]
      [Required]
      public string Title { get; set; }

      [MaxLength(255)]
      [Required]
      public string Description { get; set; }

      [Required]
      [RegularExpression("[0-9]+[/\\-\\.]?", ErrorMessage = "Rating can not contain letters")]
      public decimal Rating { get; set; }

      [MaxLength(13)]
      [Required]
      public string ISBN { get; set; }

      [Required]
      public DateTime PublicationDate { get; set; }

      public ICollection<BookAuthor>? BookAuthors { get; set; }
   }
}
