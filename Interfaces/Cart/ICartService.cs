public interface ICartService
{
    Task<CartResponseDto> AddToCartAsync(Guid userId, Guid productId, int quantity);
    Task<CartResponseDto> GetCartAsync(Guid userId);
    Task RemoveItemAsync(Guid userId, Guid cartItemId);
    Task ClearCartAsync(Guid userId);
}