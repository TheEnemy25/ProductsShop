using ProductsShop.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShop.Models
{
    [Table("Company")]
    public class Company : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Image company URL")]
        [Required(ErrorMessage = "Full image company is required")]
        public string ImageURL { get; set; }
        [Display(Name = "Name company")]
        [Required(ErrorMessage = "Full company name is required"), MaxLength(50)]
        public string CompanyName { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Full description is required"), MaxLength(100)]
        public string Description { get; set; }
        //Relationships
        public List<Product>? Products { get; set; }
    }
}
