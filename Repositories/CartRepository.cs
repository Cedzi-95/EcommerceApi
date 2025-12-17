public class CartRepository : EfRepository<Cart>
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    
}