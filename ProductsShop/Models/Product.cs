using ProductsShop.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("Product")]
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Full product name is required"), MaxLength(50)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Full URL image is required")]
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "Full description is required"), MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Unity Price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Full quantity is required")]
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        // Product category        
        [Required]
        public int CategoryId { get; set; }
        //Company
        [Required]
        public int CompanyId { get; set; }
        //Relationships
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public List<DiscountProduct>? DiscountProducts { get; set; }
        public List<ShoppingCartItem>? Carts { get; set; }
    }
}
