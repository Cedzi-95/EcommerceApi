using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/{controller}")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IUserService _userService;
    private readonly ILogger<ProductController> _logger;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService,
    CategoryService categoryService,
    IUserService userService,
    ILogger<ProductController> logger,
    IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;

    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] AddProductDto request)
    {
        try
        {
            var result = await _productService.AddAsync(request);
            var response = _mapper.Map<ProductResponseDto>(result);
            _logger.LogInformation("Added new product {request.Name}", request.Name);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add new product");
            throw;
        }
    }
}