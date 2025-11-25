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
      try
        {
            var result = await _categoryService.GetAllAsync();
            var response = _mapper.Map<List<CategoryResponseDto>>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "failed to fetch categories");
            throw;
        }
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByIdAsync(Guid categoryId)
    {
        try
        {
            var result = await _categoryService.GetByIdAsync(categoryId);
            var response = _mapper.Map<CategoryResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch this category");
            throw;
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var entity = await _categoryService.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogError($"Category {id} couldn't be found.");
            return NotFound();
        }

        var result = await _categoryService.DeleteAsynx(entity); 
        _logger.LogInformation("Category {entity.Id} has been deleted", entity.Id);    
        return Ok(result);  
    }

}