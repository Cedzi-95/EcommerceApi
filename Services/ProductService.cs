
using AutoMapper;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly CategoryService _categoryService;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository,
     CategoryService categoryService,
     ILogger<ProductService> logger,
     IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
        _logger = logger;
        _mapper = mapper;
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

        var product = _mapper.Map<Product>(addProductDto);
        await _productRepository.AddAsync(product);
        _logger.LogInformation("Added new product {product.Id}, {product.Name} in category {category.Id}",
         product.Id, product.Name, category.Id);

       var response = _mapper.Map<ProductResponseDto>(product);
       return response;

        }catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add product {productName}", addProductDto.Name);
            throw;
        }
    }

    public async Task<Product> DeleteAsync(Product product)
    {
       await _productRepository.DeleteAsync(product);
       _logger.LogInformation("Deleted product successfully");
      return product;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
       var products = await _productRepository.GetAllAsync() ?? throw new ArgumentException("Products not found");
       _logger.LogInformation("Fetched all products");
       return products;
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
        _logger.LogInformation("Fetch products from category {categoryId}", categoryId);
        return products;
    }

    public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
       var products =  await _productRepository.GetByPriceRangeAsync(minPrice, maxPrice);
       _logger.LogInformation($"found products {products.Count()} between {minPrice} and {maxPrice}");

       return products;
    }

    public Task<(IEnumerable<ProductResponseDto> Products, int TotalCount)> GetFilteredPagedAsync(string? search, Guid? categoryId, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        var product = await _productRepository.GetByIdAsync(productId)
         ?? throw new ArgumentException($"Product {productId} wasnt found");
         _logger.LogInformation("Fetch product {Id}", product.Id);
         return product;
    }

    public Task<IEnumerable<ProductResponseDto>> SearchAync(string Keyword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> UpdateAsync(Guid id, UpdateProductDto request)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id)
         ?? throw new ArgumentException($"Product {id} not found");

      
          existingProduct.Name = request.Name;
          existingProduct.Description = request.Description;
          existingProduct.Price = request.Price;
          existingProduct.IsAvailable = request.IsAvailable;
          existingProduct.StockQuantity = request.StockQuantity;
          existingProduct.CategoryId = request.CategoryId;

          await _productRepository.UpdateAsync(existingProduct);
          return existingProduct;
          
    }

    public Task UpdateStockAsync(Guid productId, int amount)
    {
        throw new NotImplementedException();
    }
}