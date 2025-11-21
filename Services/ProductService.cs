
public class ProductService : IProductService
{
   private readonly IProductRepository _productRepository;
   private readonly ICategoryRepository _categoryService;

    public ProductService(IProductRepository productRepository,
     ICategoryRepository categoryService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
    }
    public async Task<ProductResponseDto> AddAsync(AddProductDto addProductDto)
    {
         var product = new Product();
         var result = _productRepository.AddAsync(product);
         return new ProductResponseDto
         {
             ProductName = addProductDto.Name,
             Description = addProductDto.Description,
             Price = addProductDto.Price,
             StockQuantity = addProductDto.StockQuantity,
             CategoryId = addProductDto.CategoryId,
         };
         
         
        
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