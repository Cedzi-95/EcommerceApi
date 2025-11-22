
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly CategoryService _categoryService;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository,
     CategoryService categoryService,
     ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
        _logger = logger;
    }


    public async Task<ProductResponseDto> AddAsync(AddProductDto addProductDto)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(addProductDto.CategoryId);
        if (category == null)
        {
            _logger.LogWarning("Attempted to create product with non-existent category {CategoryId}", 
            addProductDto.CategoryId);
            throw new ArgumentException($"Category {addProductDto.CategoryId} does not exist.");
        }

        var product = new Product();
        await _productRepository.AddAsync(product);

        _logger.LogInformation("Created product {productId} in category {CategoryId}"
        , product.Id, addProductDto.CategoryId);

        return new ProductResponseDto
        {
            Id = product.Id,
            ProductName = addProductDto.Name,
            Description = addProductDto.Description,
            Price = addProductDto.Price,
            StockQuantity = addProductDto.StockQuantity,
            IsAvailable = true,
            CategoryId = addProductDto.CategoryId,
            CreatedAt = DateTime.UtcNow,
        };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add product {productName}", addProductDto.Name);
            throw;
        }
    }

    public Task<ProductResponseDto> DeleteAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponseDto>> GetByCategoryAsync(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponseDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<ProductResponseDto> Products, int TotalCount)> GetFilteredPagedAsync(string? search, Guid? categoryId, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponseDto> GetProductByIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponseDto>> SearchAync(string Keyword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponseDto> UpdateAsync(UpdateProductDto updateProductDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStockAsync(Guid productId, int amount)
    {
        throw new NotImplementedException();
    }
}