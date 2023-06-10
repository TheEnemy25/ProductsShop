using System.ComponentModel.DataAnnotations;

namespace ProductsShop.Data.ViewModels
{
    public class NewProductVM
    {
        public int Id { get; set; }
        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Name is required")]
        public string ProductName { get; set; }
        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Full quantity is required")]
        public int Quantity { get; set; }
        [Display(Name = "Image poster URL")]
        [Required(ErrorMessage = "Full URL image is required")]
        public string ImageURL { get; set; }
        public bool IsAvailable { get; set; }
        //Relationships
        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product category is required")]
        public int CategoryId { get; set; }

        [Display(Name = "Select a company")]
        [Required(ErrorMessage = "Product company is required")]
        public int CompanyId { get; set; }

        [Display(Name = "Select a discount")]
        public int DiscountId { get; set; }
    }
}
