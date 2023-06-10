using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Context;
using ProductsShop.Data.Repositories;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;
using System.Linq.Expressions;

namespace ProductsShop.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            _context.Categories.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var result = await _context.Categories.ToListAsync();
            return result;
        }

        public Task<IEnumerable<Category>> GetAllAsync(params Expression<Func<Category, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public Task<Category> GetByIdAsync(int id, params Expression<Func<Category, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        Task IEntityBaseRepository<Category>.UpdateAsync(int id, Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
