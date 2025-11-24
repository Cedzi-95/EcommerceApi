using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/{Controller}")]
public class CategoryController : ControllerBase
{
     private readonly CategoryService _categoryService;
    private readonly IUserService _userService;
    private readonly ILogger<CategoryController> _logger;
    private readonly IMapper _mapper;

    public CategoryController(
    CategoryService categoryService,
    IUserService userService,
    ILogger<CategoryController> logger,
     IMapper mapper)
    {
        _categoryService = categoryService;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;

    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryDto request)
    {

       var category = _mapper.Map<Category>(request);
       var response = await _categoryService.CreateAsync(category);
       return Ok(_mapper.Map<CategoryResponseDto>(response));
        
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _categoryService.GetAllAsync();
        
        var response = result.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            CategoryName = c.CategoryName,
            Description = c.Description,
            Slug = c.Slug,
            ImageUrl = c.ImageUrl,
            ParentId = c.ParentId,
            Children = []
        });

        return Ok(response);
    }

}