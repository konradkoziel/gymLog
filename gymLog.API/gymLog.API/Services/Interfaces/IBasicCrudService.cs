using gymLog.API.Model;

namespace gymLog.API.Services.interfaces
{
    public interface IBasicCrudService<T> where T : class
    {
        Task<Result<T>> CreateAsync(T entity);
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T?>> GetByIdAsync(Guid id);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result<bool>> DeleteAsync(Guid id);
    }
}