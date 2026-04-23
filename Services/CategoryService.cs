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
            if (category.ParentId.HasValue)
            {
                var parentExists = await _categoryRepository.GetByIdAsync(category.ParentId.Value);
                if (parentExists == null)
                {
                    _logger.LogInformation("ParentId '{category.ParentId}' does not exist", category.ParentId);
                    throw new ArgumentException($"ParentId '{category.ParentId}' does not exist");
                }
            }
            await _categoryRepository.AddAsync(category);
            _logger.LogInformation("Created new category {categoryName} - Id: {categoryId}",
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
            _logger.LogInformation("Succesfully fetched all categories");

            return result ?? new List<Category>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch categories");
            throw;
        }
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId)
    {
        try
        {
            return await _categoryRepository.GetSingleCategoryAsync(categoryId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something wrong, can't fetch this category");
            throw new ArgumentNullException();
        }
    }

    public async Task<Category> DeleteAsynx(Category category)
    {
        try
        {
            await _categoryRepository.DeleteAsync(category);
            _logger.LogInformation("Deleting category");
            return category;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to delete this category");
            throw new ArgumentNullException();
        }
    }

}