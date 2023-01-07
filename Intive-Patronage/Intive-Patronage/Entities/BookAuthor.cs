namespace Intive_Patronage.Entities
{
<<<<<<< HEAD
   public class BookAuthor
   {
      public int BookAuthorId { get; set; }
      public int BookId { get; set; }
      public Book Book { get; set; }
      public int AuthorId { get; set; }
      public Author Author { get; set; }
   }
=======
    public class BookAuthor
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
>>>>>>> a475901470877edfa4b24aa3277ce4c38837b2d5
}
