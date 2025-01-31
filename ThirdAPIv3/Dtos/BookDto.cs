using System.ComponentModel.DataAnnotations.Schema;
using ThirdAPI.Models;

namespace ThirdAPI.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Column (TypeName = "decimal(6,2)")]
        public required decimal Price { get; set; }
    }
}