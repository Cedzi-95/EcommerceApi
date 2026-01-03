public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserService _userService;
    private readonly IProductRepository _productRepository;
    private readonly OrderItemRepository _orderItemRepository;
    
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository,
     IUserService userService,
     IProductRepository productRepository,
     OrderItemRepository orderItemRepository,
     ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _userService = userService;
        _productRepository = productRepository;
        _orderItemRepository = orderItemRepository;
        _logger = logger;
    }
    public async Task<OrderResponseDto> CreateOrderAsync(Guid userId, CreateOrderDto request)
    {
       var user = await _userService.GetByIdAsync(userId);
       if (user == null)
        {
            _logger.LogError("User was not found");
        }

        var order = new Order
        {
            UserId = user!.Id,
            OrderedAt = DateTime.UtcNow,
            OrderStatus = Status.PENDING,
            PaymentStatus = PaymentStatus.PENDING,
            Payment = 0,
        };

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        foreach ( var itemDto in request.OrderItems!)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
            if (product == null)
            {
                _logger.LogError("Product to order is not found");
                throw new Exception("Product to order is not found");
            }
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            };

            orderItems.Add(orderItem);
            totalAmount += orderItem.UnitPrice * orderItem.Quantity;

        }
            order.Payment = totalAmount;
            order.OrderItems = orderItems;

             await _orderRepository.AddAsync(order);

            return  MapToDto(order);


    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var result = await _orderRepository.GetAllAsync();
        if (result == null)
        {
            _logger.LogError("Orders were not found");
        }
        return result ?? new List<Order>();
    }

    public async Task<Order> GetByIdAsyn(Guid orderId)
    {
       var order = await _orderRepository.GetByIdAsync(orderId);
       _logger.LogInformation("Fetch {Order.Id} successfully", order!.Id);
       if (order == null)
        {
            _logger.LogError("Order {order.Id} was not found", orderId);
        }

        return order!;
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