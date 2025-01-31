namespace ThirdAPI.Models
{
    public class Review
    {
        public int Id { get; set; } // Primary key
        public int BookId { get; set; } // Foreign key

        public string Reviewer { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public Book Book { get; set; }
    }
}