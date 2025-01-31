namespace ThirdAPI.Models
{
    public class Author
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Nationality { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}