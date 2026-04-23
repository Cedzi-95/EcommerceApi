using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService,
     ILogger<UserController> logger)
    {
        this.userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto request)
    {
        try
        {
            var response = await userService.RegisterUserAsync(request);
            _logger.LogInformation("Successfully created an account");
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Failed to create account!");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
    {
        try
        {
            var response = await userService.LoginUserAsync(request);
            _logger.LogInformation("Successfully logged in");
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            var result = await userService.GetAllAsync();
            _logger.LogInformation("Successfully fetched all users");
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Failed to fetch users!");
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{userId}/assign-role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignRoleAsync(Guid userId, [FromBody] AssignRoleDto request)
    {
        try
        {
            await userService.AssignRoleAsync(userId, request.RoleName);
            return Ok(new { message = $"Role '{request.RoleName}' assigned to user {userId}" });
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Failed to assign role");
            return BadRequest(new { message = ex.Message });
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        try
        {
            var result = await userService.GetByIdAsync(userId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Failed to fetch user {userId}", userId);
            return NotFound(new { message = ex.Message });
        }
    }



    [HttpGet("me")]
    public async Task<IActionResult> CurrentUserAsync()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        try
        {
            var result = await userService.GetByIdAsync(Guid.Parse(userId));
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Failed to fetch current user");
            return NotFound(new { message = ex.Message });
        }
    }



    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(Guid userId)
    {
        try
        {
            var result = await userService.DeleteAsync(userId);
            _logger.LogInformation("User with ID {userId} has been deleted", userId);
            return Ok($"User with ID {userId} has been deleted");
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Failed to delete user {userId}", userId);
            return BadRequest(new { message = ex.Message });
        }
    }
}