public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetCartByUserIdAsync(Guid userId);
    // Task RemoveItemAsync(Guid userId, Guid productId, int quantity);
}