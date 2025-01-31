using System.ComponentModel.DataAnnotations.Schema;

namespace ThirdAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Column (TypeName = "decimal(6,2)")]
        public required decimal Price { get; set; }
    }
}