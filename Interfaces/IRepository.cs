using System.Collections.Generic;

public interface IRepository<T>
{
    public  Task AddAsync(T entity);
    public  Task<IEnumerable<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(Guid Id);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);

}