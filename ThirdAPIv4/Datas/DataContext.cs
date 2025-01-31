using Microsoft.EntityFrameworkCore;
using ThirdAPI.Models;

namespace ThirdAPI.Datas
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {} 
        public DbSet<Book> Books { get; set;}
        public DbSet<Author> Authors { get; set;}
        public DbSet<Review> Reviews { get; set;}
        public DbSet<Publisher> Publishers { get; set;}
        public DbSet<BookPublisher> BookPublishers { get; set;}
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bir yazarın birde fazla kitabı olabilir. (1:N)
            modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

            // Bir kitabın birden fazla incelemesi olabilir. (1:N)
            modelBuilder.Entity<Book>()
            .HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<Book>()
            .Property(b => b.Price)
            .HasPrecision(18, 2);

            // Birden fazla kitap birden fazla yayınevinden yayınlanabilir. (N:N)
            modelBuilder.Entity<BookPublisher>()
            .HasKey(bp => new { bp.BookId, bp.PublisherId});

            modelBuilder.Entity<BookPublisher>()
            .HasOne(bp => bp.Book)
            .WithMany(b => b.BookPublisher)
            .HasForeignKey(bp => bp.BookId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookPublisher>()
            .HasOne(bp => bp.Publisher)
            .WithMany(p => p.BookPublisher)
            .HasForeignKey(bp => bp.PublisherId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}