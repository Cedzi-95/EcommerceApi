using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;
    private IMapper _mapper;
    private readonly IUserService _userService;

    public OrderController(IOrderService orderService,
    ILogger<OrderController> logger,
    IMapper mapper,
    IUserService userService)
    {
        _orderService = orderService;
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderDto request)
    {
        try
        {
        var customer = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _orderService.CreateOrderAsync(customer, request);
        _logger.LogInformation("Order confirmed");
        return Ok(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to place an order");
            throw;
        }
        

    }
}