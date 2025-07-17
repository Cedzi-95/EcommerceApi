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
        
    }
}