public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CartService> _logger;

    public CartService(ICartRepository cartRepository,
    IProductRepository productRepository,
    ILogger<CartService> logger)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _logger = logger;
    }
    public async Task<CartResponseDto> AddToCartAsync(Guid userId, Guid productId, int quantity)
    {
       try
        {
            var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
        {
            _logger.LogWarning("Product not found");
            throw new ArgumentException("Product not found");
        }
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            // create new cart if user doesnt have one
            cart = new Cart { UserId = userId };
            _logger.LogInformation("Create new cart for user {userId}", userId);
        }

        var existingItem = cart.CartItems?
        .FirstOrDefault(ci => ci.ProductId == productId);

        if (existingItem != null)
        {
            // increase quantity if product exists already in the cart
            existingItem.Quantity += quantity;
            await _cartRepository.UpdateAsync(cart);
            _logger.LogInformation("Added product in cart");
        }
        else
        {
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity
            };
            cart.CartItems?.Add(cartItem);
            await _cartRepository.UpdateAsync(cart);
        }
        _logger.LogInformation("Successfully created new cart");
        return MapToDto(cart);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to Add new Cart");
            throw new ArgumentException(ex.Message);
        }
    }

    public Task ClearCartAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<CartResponseDto> GetCartAsync(Guid userId)
    {
       
       try 
       {
         var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        _logger.LogInformation("Fetching Cart for user {userId}", userId);
        if (cart == null)
        {
            _logger.LogWarning("Cart not found");
            throw new ArgumentException("Cart not found");
        }
        return MapToDto(cart);
        }

        catch(Exception ex)
        {
            _logger.LogError(ex, "Could not fetch cart");
            throw new ArgumentException(ex.Message);
        }
    }

    public Task RemoveItemAsync(Guid userId, Guid cartItemId)
    {
        throw new NotImplementedException();
    }

    private CartResponseDto MapToDto(Cart cart)
    {
        return new CartResponseDto
        {
            CartId = cart.Id,
            UserId = cart.UserId,
            CartItems = cart.CartItems?.Select(ci => new CartItemResponseDto
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product?.Name,
                Quantity = ci.Quantity
            }).ToList()
        };
    }
}