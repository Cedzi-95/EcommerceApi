public class CartRepository : EfRepository<Cart>
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    
}

public class CartItemRepository : EfRepository<CartItem>
{
    public CartItemRepository(AppDbContext context) : base(context)
    {
    }
}