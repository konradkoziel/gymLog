using gymLog.API.Model.DTO;

namespace gymLog.API.Services.interfaces
{
    public interface IBasicCrudService<T> where T : class
    {
        Task<Result<T>> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}