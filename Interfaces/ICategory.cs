public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetSingleCategoryAsync(Guid id);
}

public interface ICategoryService
{
    
}