namespace ThirdAPI.Dtos
{
    public class ReviewDto
    {
        public int Id { get; private set; }

        public int BookId { get; set; }
        public string Reviewer { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public void SetId(int id)
    {
        Id = id;
    }
    }
}