using ProductsShop.Data.Repositories;
using ProductsShop.Data.ViewModels;
using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<NewProductDropdownsVM> GetNewProductDropdownsValues();
        Task AddNewProductAsync(NewProductVM data);
        Task UpdateProductAsync(NewProductVM data);
    }
}
