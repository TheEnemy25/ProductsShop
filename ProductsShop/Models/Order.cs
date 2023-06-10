using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Status { get; set; }
        //ApplicationUser
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        //Relationships
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
//public bool IsDeleted { get; set; } = false;