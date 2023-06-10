using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("DiscountProduct")]
    public class DiscountProduct
    {
        [Key]
        public int Id { get; set; }
        // Relationship
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int DiscountId { get; set; }
        public Discount? Discount { get; set; }
    }
}
