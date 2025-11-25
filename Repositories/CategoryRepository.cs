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
}