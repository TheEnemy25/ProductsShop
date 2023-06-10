using ProductsShop.Data.Repositories;
using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface ICompaniesService
    {
        public Task AddAsync(Company entity);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<Company>> GetAllAsync();
        public Task<Company> GetByIdAsync(int id);
        public Task<Company> UpdateAsync(int id, Company entity);
    }
}
