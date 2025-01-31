namespace ThirdAPI.Dtos
{
    public class BookDto
    {
        public int Id { get; private set; }
        public int AuthorId { get; set; }
        public required string Name { get; set; }
        public int PublishingYear { get; set; }
        public decimal Price { get; set; }

        public void SetId(int id)
    {
        Id = id;
    }
    }
}