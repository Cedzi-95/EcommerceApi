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
             return BadRequest();
        }

    }

    [HttpGet("GetAll")]
    [Authorize]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var orders = await _orderService.GetAllAsync();
            _logger.LogInformation("Fetched all orders");
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch orders");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{orderId}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(Guid orderId)
    {
        try
        {
            var order = await _orderService.GetByIdAsyn(orderId);
            _logger.LogInformation("Fetched order {order.Id}", order.Id);
            return Ok(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong trying to fetch this order");
            return BadRequest(ex.Message);
        }
    }
}