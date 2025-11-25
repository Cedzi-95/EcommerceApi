public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserService _userService;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository,
    IUserService userService,
     ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _userService = userService;
        _logger = logger;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        try
        {
            // Category-objektet kommer redan färdigt från controller
            await _categoryRepository.AddAsync(category);
            _logger.LogInformation("Created new category {categoryName} {categoryId}", 
                category.CategoryName, category.Id);
            
            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create new category {categoryName}", 
                category.CategoryName);
            throw;
        }
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        try
        {
            var result = await _categoryRepository.GetCategoriesAsync();
       
        
        return  result ?? new List<Category>();
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch categories");
            throw;
        }       
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId)
    {
        return await _categoryRepository.GetSingleCategoryAsync(categoryId);
    }
}