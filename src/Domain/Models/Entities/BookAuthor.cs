namespace Intive_Patronage.Entities
{
   public class BookAuthor
   {
      public int BookAuthorId { get; set; }
      public int BookId { get; set; }
      public Book Book { get; set; }
      public int AuthorId { get; set; }
      public Author Author { get; set; }
   }
}
