namespace Intive_Patronage.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public List<BookAuthor> BookAuthor { get; set; } = new List<BookAuthor>();

        public Book(string title, string description, decimal rating, string iSBN, DateTime publicationDate)
        {
            Title = title;
            Description = description;
            Rating= rating;
            ISBN = iSBN;
            PublicationDate = publicationDate;
        }
    }


}
