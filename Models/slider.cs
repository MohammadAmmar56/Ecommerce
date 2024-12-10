using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class slider
    {
        [Key]
        public int id { get; set; }
        public string? heading {  get; set; }
        public string? image { get; set; }
        public string? sliderlink { get; set; }
    }
}
