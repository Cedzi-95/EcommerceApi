using EcommerceApi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    public Task<RegisterUserResponse> RegisterUserAsync(RegisterUserDto request);
    public Task<string> LoginUserAsync(LoginRequestDto request);
    public Task<IEnumerable<UserEntity>> GetAllAsync();
    public Task<UserEntity> GetByIdAsync(Guid userId);
    public Task<UserEntity> DeleteAsync(Guid userId);
    public Task AssignRoleAsync(Guid userId, string roleName);
}





public class UserService : IUserService
{
    private readonly AuthHelpers authHelpers;
    private readonly UserManager<UserEntity> userManager;
    private readonly SignInManager<UserEntity> signInManager;
    private readonly  RoleManager<IdentityRole<Guid>> roleManager;


    public UserService(AuthHelpers authHelpers, 
    UserManager<UserEntity> userManager,
     SignInManager<UserEntity> signInManager,
      RoleManager<IdentityRole<Guid>> roleManager)
    {
        this.authHelpers = authHelpers;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }
    public async Task<UserEntity> DeleteAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException("user not found");
        var result = await userManager.DeleteAsync(user!);
        if (result.Succeeded)
        {
            return null!;
        }
        throw new Exception($"Could not delete user {user!.Id}");

    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await userManager.Users.ToListAsync();
    }

    public async Task<UserEntity> GetByIdAsync(Guid userId)
    {
        return await userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException("User not found");
    }


    public async Task<string> LoginUserAsync(LoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user == null)
        {
            throw new ArgumentException("user not found");
        }
        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            request.Password!,
            false
            );
        if (!result.Succeeded)
        {
            throw new ArgumentException("Invalid credentials");
        }
        var token =  await authHelpers.GenerateJWTToken(user);
        return token;


    }

    public async Task AssignRoleAsync(Guid userId, string roleName)
{
    var user = await userManager.FindByIdAsync(userId.ToString());
    if (user == null)
        throw new ArgumentException("User not found");

    var roleExists = await roleManager.RoleExistsAsync(roleName);
    if (!roleExists)
        throw new ArgumentException($"Role '{roleName}' does not exist");

    var result = await userManager.AddToRoleAsync(user, roleName);
    if (!result.Succeeded)
        throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.Description)));
}



    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserDto request)
    {
        var user = new UserEntity()
        {
            Email = request.Email,
            UserName = request.UserName,
            PasswordHash = request.Password
        };

        var result = await userManager.CreateAsync(user, request.Password!);

        if (!result.Succeeded)
        {
            var erromessages = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new ArgumentException($"Error registering user: {erromessages}");
        }

        // Ge nya användare "User" rollen
    await userManager.AddToRoleAsync(user, "User");

    var token = await authHelpers.GenerateJWTToken(user); 

        return new RegisterUserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName
        };


    }
}