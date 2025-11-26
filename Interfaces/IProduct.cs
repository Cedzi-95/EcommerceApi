public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsynx(Guid categoryId);
    Task<IEnumerable<Product>> SearchAync(string Keyword);
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<Product>> GetProductsOrderedByPriceAsync(bool ascending);
    Task<IEnumerable<Product>> GetNewestProductsAsync(int count);
    Task<IEnumerable<Product>> GetOutOfStockProductsAsync();
    Task UpdateStockAsync(Guid productId, int amount);
    Task<IEnumerable<Product>> GetPagedAsync(int pageNumber, int pageSize);
    Task<IEnumerable<Product>> GetRelatedProductsAsync(Guid productId, int count);
    Task<(IEnumerable<Product> Products, int TotalCount)>GetFilteredPagedAsync
    (string? search, Guid? categoryId, int page, int pageSize);

}


public interface IProductService
{
    Task<ProductResponseDto> AddAsync(AddProductDto addProductDto);
    Task<Product> GetProductByIdAsync(Guid productId);
     Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<ProductResponseDto> UpdateAsync(UpdateProductDto updateProductDto);
    Task<Product> DeleteAsync(Product product);
    Task<bool> SoftDeleteAsync(Guid productId);
    Task<IEnumerable<ProductResponseDto>> GetByCategoryAsync(Guid categoryId);
    Task<IEnumerable<ProductResponseDto>> SearchAync(string Keyword);
    Task<(IEnumerable<ProductResponseDto> Products, int TotalCount)>GetFilteredPagedAsync
    (string? search, Guid? categoryId, int page, int pageSize);
    Task<IEnumerable<ProductResponseDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task UpdateStockAsync(Guid productId, int amount);


}