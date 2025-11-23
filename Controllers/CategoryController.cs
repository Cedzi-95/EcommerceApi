using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/{Controller}")]
public class CategoryController : ControllerBase
{
     private readonly CategoryService _categoryService;
    private readonly IUserService _userService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(
    CategoryService categoryService,
    IUserService userService,
    ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _userService = userService;
        _logger = logger;

    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryDto request)
    {
        var category = await _categoryService.CreateAsync(request);
        return Ok(category);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result);
    }
}