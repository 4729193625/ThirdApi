namespace ThirdAPI.Dtos
{
    public class AuthorDto
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Nationality { get; set; }

        public void SetId(int id)
    {
        Id = id;
    }
    }
}