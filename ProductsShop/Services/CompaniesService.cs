using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Context;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Services
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ApplicationDbContext _context;

        public CompaniesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company entity)
        {
            await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            _context.Companies.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            var result = await _context.Companies.ToListAsync();
            return result;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            var result = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<Company> UpdateAsync(int id, Company entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
