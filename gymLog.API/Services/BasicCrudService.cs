using gymLog.API.Model.DTO;
using gymLog.Entity;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services
{
    public abstract class BasicCrudService<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BasicCrudService(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Create a new entity
        public virtual async Task<Result<T>> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return Result<T>.Success(entity, "Data created successfully");
        }

        // Get all entities
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Get entity by ID
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Update an existing entity
        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Delete an entity by ID
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
