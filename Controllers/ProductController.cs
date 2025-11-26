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

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
        var products = await _productService.GetAllProductsAsync();
        _logger.LogInformation("Fetched all products");
        var response = _mapper.Map<List<ProductResponseDto>>(products);
        return Ok(response);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch products");
            throw;
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
       { 
        var product = await _productService.GetProductByIdAsync(id);
        _logger.LogInformation("Fethed product {product.Id}", product.Id);
        var response = _mapper.Map<ProductResponseDto>(product);
        return Ok(response);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch product");
            throw;
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try 
        {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            _logger.LogError("No product with id {product.Id} was found", product!.Id);
            return NotFound();
        }

        var delete = await _productService.DeleteAsync(product);
        _logger.LogInformation("Deleted product {product.Id} successfully!", product.Id);

        var response = _mapper.Map<ProductResponseDto>(delete);
        
        return Ok(response);

        } catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete product");
            throw;
        }
    }
}