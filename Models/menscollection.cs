using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class menscollection
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? image { get; set; }
        public string? price1 { get; set; }
        public string? price2 { get; set; }
        public string? discount { get; set; }
    }
}
