public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserService _userService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository,
     IUserService userService,
     ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _userService = userService;
        _logger = logger;
    }
    public async Task<Order> CreateOrderAsync(Guid userId, CreateOrderDto request)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user == null)
        {
            _logger.LogError("User not found");
        }
        var order = new CreateOrderDto
        {
            UserId = user!.Id
        };

       var result = await _orderRepository.AddAsync();
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByIdAsyn(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }
}