
using Microsoft.EntityFrameworkCore;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredPagedAsync(string? search, Guid? categoryId, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetNewestProductsAsync(int count)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetOutOfStockProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetPagedAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsynx(Guid categoryId)
    {
        return await _context.Set<Product>()
        .Where(p => p.CategoryId == categoryId)
        .OrderBy(p => p.Name)
        .ToListAsync();
    }

    public Task<IEnumerable<Product>> GetProductsOrderedByPriceAsync(bool ascending)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetRelatedProductsAsync(Guid productId, int count)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> SearchAync(string Keyword)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStockAsync(Guid productId, int amount)
    {
        throw new NotImplementedException();
    }
}