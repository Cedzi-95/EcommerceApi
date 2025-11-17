using Microsoft.AspNetCore.Identity;

public class UserEntity : IdentityUser<Guid>
{
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}



public class RegisterUserDto
{
    public required string? Email { get; set; }
    public required string? UserName { get; set; }
    public required string? Password { get; set; }

}

public class RegisterUserResponse
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class LoginRequestDto
{
    public required string? Email { get; set; }
    public required string? Password { get; set; }

}
public class LoginResponseDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }

}

public class AssignRoleDto
{
    public string RoleName { get; set; } = string.Empty;
}