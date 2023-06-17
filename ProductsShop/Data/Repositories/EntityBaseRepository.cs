using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductsShop.Data.Context;
using System.Linq.Expressions;

namespace ProductsShop.Data.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbContext _context;
        public EntityBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                if (includeProperty.Body is MemberExpression memberExpression)
                {
                    query = query.Include(memberExpression.Member.Name);
                }
                else if (includeProperty.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
                {
                    query = query.Include(operand.Member.Name);
                }
                else
                {
                    throw new ArgumentException("Invalid include property expression.");
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                if (includeProperty.Body is MemberExpression memberExpression)
                {
                    query = query.Include(memberExpression.Member.Name);
                }
                else if (includeProperty.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
                {
                    query = query.Include(operand.Member.Name);
                }
                else
                {
                    throw new ArgumentException("Invalid include property expression.");
                }
            }

            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}