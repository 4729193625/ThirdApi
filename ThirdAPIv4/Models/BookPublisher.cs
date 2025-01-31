namespace ThirdAPI.Models
{
    public class BookPublisher
    {
        public int BookId { get; set; } // Foreign key
        public int PublisherId { get; set; } // Foreign key

        // Navigation Properties
        public Book Book { get; set; }
        public Publisher Publisher { get; set; }
    }
}