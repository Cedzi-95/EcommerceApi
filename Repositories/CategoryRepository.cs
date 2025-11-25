using Microsoft.EntityFrameworkCore;

public class CategoryRepository : EfRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
        .Where(c => c.ParentId == null)
        .Include(c => c.Children!)
        .ThenInclude(c => c.Children!)
        .ThenInclude(c => c.Children)
        .ToListAsync();
        
        return categories;
    }

    public async Task<Category?> GetSingleCategoryAsync(Guid id)
    {
      return await _context.Set<Category>()
      .Where(c => c.Id == id)
      .Include(c => c.Children!)
      .ThenInclude(c => c.Children)
      .FirstOrDefaultAsync();
        
    }
}