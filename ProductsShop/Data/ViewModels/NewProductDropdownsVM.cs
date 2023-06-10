using ProductsShop.Models;

namespace ProductsShop.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public NewProductDropdownsVM()
        {
            Categories = new List<Category>();
            Companies = new List<Company>();
        }
        public List<Category> Categories { get; set; }
        public List<Company> Companies { get; set; }
    }
}
