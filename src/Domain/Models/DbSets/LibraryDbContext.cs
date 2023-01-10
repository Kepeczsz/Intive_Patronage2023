using Intive_Patronage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Models.DbSets
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
            modelBuilder.Entity<BookAuthor>()
                    .HasOne(ba => ba.Book)
                    .WithMany(b => b.BookAuthors)
                    .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);
        }
    }
}
