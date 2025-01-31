namespace ThirdAPI.Models
{
    public class Book
    {
        public int Id { get; set; } // Primary key 
        public int AuthorId { get; set; } // Foreign key

        public required string Name { get; set; }
        public int PublishingYear { get; set; }
        public decimal Price { get; set; }


        public Author Author { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<BookPublisher> BookPublisher { get; set; } = new List<BookPublisher>();
    }
}