public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
}

public interface ICategoryService
{
    
}