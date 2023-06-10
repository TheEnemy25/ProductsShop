using ProductsShop.Data.Repositories;
using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task AddAsync(Category entity);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(int id);
        public Task<Category> UpdateAsync(int id, Category entity);
    }
}
