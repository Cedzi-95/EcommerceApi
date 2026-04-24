public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetCartByUserIdAsync(Guid userId);
}