using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class signup
    {
        [Key]
        public int id { get; set; }
        public string? fullname { get; set; }
        public string? email { get; set; }
        public string? mobile { get; set; }
        public string? password { get; set; }
        public string? confirmpass { get; set; }
    }
}
