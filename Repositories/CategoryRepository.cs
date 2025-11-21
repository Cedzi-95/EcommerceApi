public class CategoryRepository : EfRepository<Category>, ICategory
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}