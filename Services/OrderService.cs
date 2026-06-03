public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserService _userService;
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository,
     IUserService userService,
     IProductRepository productRepository,
     ICartRepository cartRepository,
     IEmailService emailService,
     ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _userService = userService;
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _emailService = emailService;
        _logger = logger;
    }
    public async Task<OrderResponseDto> CreateOrderAsync(Guid userId, CreateOrderDto request)
    {
        try
        {
            //fetch cart with items and products
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
            {
                _logger.LogWarning("Cart is empty or doesn't exist");
                throw new InvalidOperationException("Cart is empty or does not exist");
            }

            var orderItems = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.Product!.Price
            }).ToList();

            // check the stock 
            foreach (var ci in cart.CartItems)
            {
                if (ci.Product!.StockQuantity < ci.Quantity)
                    throw new InvalidOperationException(
                        $"Not enough stock for '{ci.Product.Name}'. Available: {ci.Product.StockQuantity}, Requested: {ci.Quantity}");
            }

            // Decrease the stock quantity
            foreach (var ci in cart.CartItems)
            {
                ci.Product!.StockQuantity -= ci.Quantity;
                await _productRepository.UpdateAsync(ci.Product);
            }

            var order = new Order
            {
                UserId = userId,
                OrderItems = orderItems,
                Payment = orderItems.Sum(i => i.UnitPrice * i.Quantity),
                OrderStatus = Status.PENDING,
                PaymentStatus = PaymentStatus.PENDING,
                OrderedAt = DateTime.UtcNow
            };

            await _orderRepository.AddAsync(order);

            //send confirmation email
            var user = await _userService.GetByIdAsync(userId);
            await _emailService.SendOrderConfirmationAsync(user.Email!, MapToDto(order));
            _logger.LogInformation("Send confirmation email to {user.Email}", user.Email);

            //Clear the cart after creating an order
            cart.CartItems.Clear();
            await _cartRepository.UpdateAsync(cart);
            _logger.LogInformation("Order {OrderId} created for user {UserId}", order.Id, userId);

            return MapToDto(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        try
        {
            var result = await _orderRepository.GetAllAsync();
            if (result == null)
            {
                _logger.LogError("Orders were not found");
            }
            return result ?? new List<Order>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while trying to fetch orders");
            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<OrderResponseDto> GetByIdAsyn(Guid orderId)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException($"Order {orderId} not found");

            _logger.LogInformation("Fetched order {OrderId}", order.Id);
            return MapToDto(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserAsync(Guid userId)
    {
        try
        {
            var foundUser = await _userService.GetByIdAsync(userId);
            _logger.LogInformation("Fetched user {userId}", userId);

            if (foundUser == null)
            {
                _logger.LogError("User not found");
                throw new KeyNotFoundException("User not found");
            }

            var orders = await _orderRepository.GetOrdersByUserAsync(foundUser!.Id);

            _logger.LogInformation("Found {count} orders for user {userId} ", orders.Count(), foundUser.Id);
            return orders.Select(o => MapToDto(o));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            throw new ArgumentException(ex.Message);
        }
    }

    public async Task OrderStatusAsync(Guid orderId, Status newOrderStatus)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogWarning("Order not found");
                throw new KeyNotFoundException($"Order {orderId} not found");

            }
            await _orderRepository.OrderStatusAsync(orderId, newOrderStatus);

            _logger.LogInformation("Updated order {OrderId} to status {Status}", orderId, newOrderStatus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            throw new ArgumentException(ex.Message);
        }
    }

    public async Task PaymentStatusAsync(Guid OrderId, PaymentStatus paymentstatus)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(OrderId);
            if (order == null)
            {
                _logger.LogWarning("Order not found");
                throw new KeyNotFoundException();
            }
            await _orderRepository.PaymentStatusAsync(OrderId, paymentstatus);
            _logger.LogInformation("Update order {OrderId} to payment status {status}", OrderId, paymentstatus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            throw new ArgumentException(ex.Message);
        }
    }

    public Task<Order> UpdateAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    private OrderResponseDto MapToDto(Order order)
    {
        return new OrderResponseDto
        {
            OrderId = order.Id,
            UserId = order.UserId,
            OrderedAt = order.OrderedAt,
            OrderStatus = order.OrderStatus,
            PaymentStatus = order.PaymentStatus,
            TotalAmount = order.Payment,
            OrderItems = order.OrderItems?.Select(oi => new OrderItemResponseDto
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                RowTotal = oi.Quantity * oi.UnitPrice
            }).ToList()
        };
    }
}