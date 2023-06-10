using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductsShop.Data.Repositories;

namespace ProductsShop.Models
{
    [Table("Category")]
    public class Category : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Image category URL")]
        [Required(ErrorMessage = "Full category logo is required")]
        public string ImageURL { get; set; }
        [Display(Name = "Category name")]
        [Required(ErrorMessage = "Full category name is required"), MaxLength(50)]
        public string CategoryName { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Full category description is required")]
        public string Description { get; set; }
        //Relationships
        public List<Product>? Products { get; set; }
    }
}
