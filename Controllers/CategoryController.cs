using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/{Controller}")]
public class CategoryController : ControllerBase
{
     private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(
    ICategoryService categoryService,
    IUserService userService,
    ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _userService = userService;
        _logger = logger;

    }
}