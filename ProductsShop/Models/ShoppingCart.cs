using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        //05.06.2023Update
        public Product Product { get; set; }
        public string ShoppingCartId { get; set; }
    }
}