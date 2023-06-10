using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Discrount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //Relationship
        public List<DiscountProduct>? DiscountProducts { get; set; }
    }
}
