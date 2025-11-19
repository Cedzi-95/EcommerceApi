using System.Collections.Generic;

public interface IRepository<T> where T : class
{
    public  Task AddAsync(T entity);
    public  Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(Guid id);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);

}
