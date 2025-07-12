using Microsoft.AspNetCore.Identity;

public class UserEntity : IdentityUser<Guid>
{
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}