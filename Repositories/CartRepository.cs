using Microsoft.EntityFrameworkCore;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .Include(c => c.CartItems!)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}

public class CartItemRepository : EfRepository<CartItem>, ICartItemRepository
{
    public CartItemRepository(AppDbContext context) : base(context)
    {
    }
}