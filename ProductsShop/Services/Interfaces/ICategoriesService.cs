using ProductsShop.Data.Repositories;
using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface ICategoriesService : IEntityBaseRepository<Category>
    {
        new Task<Category> UpdateAsync(int id, Category entity);
    }
}
