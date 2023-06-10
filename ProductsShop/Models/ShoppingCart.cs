using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfProducts { get; set; }
        public double TotalPrice { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}