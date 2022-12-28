namespace Intive_Patronage.Entities
{
    public class BookAuthor
    {
        // klucz obcy właściwości nawigacyjnej Employee
        public int AuthorId { get; set; }

        // Właściwość nawigacyjna encji Employee
        public Author Author { get; set; }


        // klucz obcy właściwości nawigacyjnej Project
        public int BookId { get; set; }

        // Właściwość nawigacyjna encji Project
        public Book Book { get; set; }

       
    }
}
