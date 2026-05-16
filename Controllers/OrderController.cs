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

            if (order == null)
                throw new KeyNotFoundException($"Order {orderId} not found");
            _logger.LogInformation("Fetched order {OrderId}", orderId);
            return Ok(order);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong trying to fetch this order");
            return StatusCode(500, "Something went wrong");
        }
    }

    [HttpGet("my-orders")]
    [Authorize]
    public async Task<IActionResult> GetOrdersByUserAsync()
    {
        try
        {
            var user = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _orderService.GetOrdersByUserAsync(user);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            return StatusCode(500, "Something went wrong");
        }
    }

    [HttpPut("OrderStatus")]
    [Authorize]
    public async Task<IActionResult> OrderStatusAsync([FromQuery] Guid orderId, Status status)
    {
        try
        {
            await _orderService.OrderStatusAsync(orderId, status);
            return Ok($"Order status updated to {status}");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            return StatusCode(500, "Something went wrong");
        }
    }

     [HttpPut("PaymentStatus")]
    [Authorize]
    public async Task<IActionResult> PaymentStatusAsync([FromQuery] Guid orderId, PaymentStatus status)
    {
        try
        {
            await _orderService.PaymentStatusAsync(orderId, status);
            return Ok($"Order paymentstatus updated to {status}");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            return StatusCode(500, "Something went wrong");
        }
    }


}