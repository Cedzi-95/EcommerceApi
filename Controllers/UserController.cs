using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto request)
    {
        try
        {
            var response = await userService.RegisterUserAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
    {
        try
        {
             var response = await userService.LoginUserAsync(request);
        return Ok(response);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            var result = await userService.GetAllAsync();
            return Ok(result);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }

    [Authorize(Roles = "Admin")]
[HttpPost("{userId}/assign-role")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> AssignRoleAsync(string userId, [FromBody] AssignRoleDto request)
{
    try
    {
        await userService.AssignRoleAsync(userId, request.RoleName);
        return Ok(new { message = $"Role '{request.RoleName}' assigned to user {userId}" });
    }
    catch (ArgumentException ex)
    {
        return BadRequest(new { message = ex.Message });
    }
}


    [Authorize(Roles = "Admin")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetById(string userId)
    {
        try
        {
            var result = await userService.GetByIdAsync(userId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message});
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
            var result = await userService.GetByIdAsync(userId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message});
        }
    }



    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteAsync(string userId)
    {
         try
        {
            var result = await userService.DeleteAsync(userId);
            return Ok($"User with ID {userId} has been deleted");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }
}