using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("user")]
public class UserController : ControllerBase
{
    public IUserService userService;

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

    [Authorize]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
    {
        var response = await userService.LoginUserAsync(request);
        return Ok(response);
    }
}