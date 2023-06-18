using ProductsShop.Data.Repositories;
using ProductsShop.Data.ViewModels;
using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<NewProductDropdownsVM> GetNewProductDropdownsValues();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
        Task AddNewProductAsync(NewProductVM data);
        Task UpdateProductAsync(NewProductVM data);
    }
}
