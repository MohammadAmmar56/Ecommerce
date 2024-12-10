using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class collection
    {
        [Key]
        public int id { get; set; }
        public string? image { get; set; }
        public string? collectionname { get; set; }
        public string? collectionlink { get; set; }
    }
}
