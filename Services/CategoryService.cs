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

    public async Task<CategoryResponseDto> CreateAsync(CategoryDto request)
    {
        try
        {
            var category = new Category();
        await _categoryRepository.AddAsync(category);
        _logger.LogInformation("Created new category {categoryName} {categoryId}", category.Id, category.CategoryName);

        return new CategoryResponseDto
        {
            Id = category.Id,
            CategoryName = request.CategoryName,
            Description = request.Description,
            Slug = request.Slug,
            ImageUrl = category.ImageUrl
        };
        } catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to create new category {categoryName}", request.CategoryName);
            throw;
        }
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        try
        {
            var result= _categoryRepository.GetAllAsync();
        if (result == null)
        {
            _logger.LogError ("Failed to fetch categories");
            throw new ArgumentException ("No categories were found");
        }

        
        return (IEnumerable<CategoryResponseDto>)await result;
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch categories");
            throw;
        }       
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId)
    {
        return await _categoryRepository.GetByIdAsync(categoryId);
    }
}