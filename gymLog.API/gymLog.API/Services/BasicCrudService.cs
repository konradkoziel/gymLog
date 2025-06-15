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
        public virtual async Task<Result<IEnumerable<T>>> GetAllAsync()
        {
            List<T> entities = await _dbSet.ToListAsync();
            return Result<IEnumerable<T>>.Success(entities, "Data collections returned successfully");
        }

        // Get entity by ID
        public virtual async Task<Result<T?>> GetByIdAsync(Guid id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return Result<T?>.Failure("Entity not found", []);
            return Result<T?>.Success(entity, "Data returned successfully");
        }

        // Update an existing entity
        public virtual async Task<Result<T?>> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return Result<T?>.Success(entity, "Data updated successfully");
        }

        // Delete an entity by ID
        public virtual async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return Result<bool>.Failure("Data not found", null);
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true, "Data deleted successfully");
        }
    }
}
