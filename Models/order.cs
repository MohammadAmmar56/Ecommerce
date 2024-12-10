using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class order
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? pincode { get; set; }
        public string? productname { get; set; }
        public string? productprice { get; set; }
        public string? state { get; set; }
        public string? address1 { get; set; }
        public string? address2 { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public string? productid { get; set; }
        public string? paymentmode { get; set; }
        public string? paymentstatus { get; set; }
        public string? transactionid { get; set; }

    }
}
