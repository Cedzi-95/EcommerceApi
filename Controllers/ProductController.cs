using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/{controller}")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService,
    ICategoryService categoryService,
    IUserService userService,
    ILogger<ProductController> logger)
    {
        _productService = productService;
        _categoryService = categoryService;
        _userService = userService;
        _logger = logger;

    }
}