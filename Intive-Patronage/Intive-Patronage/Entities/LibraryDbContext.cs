using Microsoft.EntityFrameworkCore;

namespace Intive_Patronage.Entities
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-QASAA08\\SZCZEPEK;Initial Catalog=Intive_Patronage;" +
                "Integrated Security=True;TrustServerCertificate=True");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.AuthorId, ba.BookId });
        }
    }
}
