using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]

public class PaymentController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;
    private readonly IConfiguration _config;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IOrderService orderService,
    IPaymentService paymentService,
   IConfiguration config,
    ILogger<PaymentController> logger)
    {
        _orderService = orderService;
        _paymentService = paymentService;
        _config = config;
        _logger = logger;
    }

    [HttpPost("create-intent")]
    [Authorize]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentDto dto)
    {
        try
        {
            var clientSecret = await _paymentService.CreatePaymentIntentAsync(dto.Amount);
            return Ok( new PaymentIntentResponseDto
            {
                ClientSecret = clientSecret,
                PublishableKey = _config["Stripe:PublishableKey"]!
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create payment intent");
            return StatusCode(500, "Payment initialization failed");
        }
    }

    [HttpPost("confirm")]
    [Authorize]
    public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmOrderPaymentDto dto)
    {
        try
        {
            var success = await _paymentService.ConfirmPaymentAsync(dto.PaymentIntentId);
           if (!success)
            {
                return BadRequest("Payment not succeeded in Stripe");
            }

            await _orderService.PaymentStatusAsync(dto.OrderId, PaymentStatus.PAID);
            await _orderService.OrderStatusAsync(dto.OrderId, Status.CONFIRMED);
            
            _logger.LogInformation("Order {orderId} marked as paid and confirmed", dto.OrderId);
            return Ok(new { message = "Payment confirmed", orderId = dto.OrderId});
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to confirm payment");
            return StatusCode(500, "Payment confirmation failed");
        }
    }
}