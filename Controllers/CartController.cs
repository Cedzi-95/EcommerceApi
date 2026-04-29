using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IUserService _userService;
    private readonly ILogger<CartController> _logger;

    public CartController(ICartService cartService,
    IUserService userService,
    ILogger<CartController> logger)
    {
        _cartService = cartService;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("Add")]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] AddToCartDto addToCartDto)
    {
        try
        {
            var user = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _cartService.AddToCartAsync(user, addToCartDto.ProductId, addToCartDto.Quantity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong, can't add item to cart");
            return BadRequest(ex.Message);
        }
    }
}
