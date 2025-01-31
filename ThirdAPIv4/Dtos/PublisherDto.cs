namespace ThirdAPI.Dtos
{
    public class PublisherDto
    {
        public int Id { get; private set; }
        public string Name { get; set; }
    

    public void SetId(int id)
    {
        Id = id;
    }
    }
}