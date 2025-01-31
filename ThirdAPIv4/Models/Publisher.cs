namespace ThirdAPI.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookPublisher> BookPublisher { get; set; } = new List<BookPublisher>();
    }
}