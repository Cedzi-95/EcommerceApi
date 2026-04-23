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
    private readonly ILogger<UserService> _logger;


    public UserService(AuthHelpers authHelpers, 
    UserManager<UserEntity> userManager,
     SignInManager<UserEntity> signInManager,
      RoleManager<IdentityRole<Guid>> roleManager,
      ILogger<UserService> logger)
    {
        this.authHelpers = authHelpers;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        _logger = logger;
    }
    public async Task<UserEntity> DeleteAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException("user not found");
        _logger.LogInformation("Deleting user {user.Id}", user.Id);
        var result = await userManager.DeleteAsync(user!);
        if (result.Succeeded)
        {
            return null!;
        }
        throw new Exception($"Could not delete user {user!.Id}");

    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        try 
        {
            var result = await userManager.Users.ToListAsync();
            if (result == null)
            {
                _logger.LogWarning("Can't find users");
                throw new ArgumentNullException();
            }
            _logger.LogInformation("Fetching all the users");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Somehting went wrong");
            return null!;
        }
    }

    public async Task<UserEntity> GetByIdAsync(Guid userId)
    {
      try
      {
        _logger.LogInformation("Fetching user {userId}", userId);
          return await userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException("User not found");
      }
      catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch user");
            return null!;
        }
    }


    public async Task<string> LoginUserAsync(LoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user == null)
        {
            _logger.LogWarning("User not found");
            throw new ArgumentException("user not found, create an account first");
        }
        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            request.Password!,
            false
            );
        if (!result.Succeeded)
        {
            _logger.LogInformation("Invalid password or email");
            throw new ArgumentException("Invalid credentials");
        }
        _logger.LogInformation("Successfully generated JWT token");
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

  _logger.LogInformation("Successfully assigned role {RoleName} to user {userId}", roleName, userId);
    var result = await userManager.AddToRoleAsync(user, roleName);
    if (!result.Succeeded)
        throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.Description)));
}



    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserDto request)
    {
      try
      {
          var user = new UserEntity()
        {
            Email = request.Email,
            UserName = request.UserName,
            PasswordHash = request.Password
        };

        var result = await userManager.CreateAsync(user, request.Password!);
        _logger.LogInformation("Creating account for {user.UserName}", user.UserName);

        if (!result.Succeeded)
        {
            var erromessages = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new ArgumentException($"Error registering user: {erromessages}");
        }

    await userManager.AddToRoleAsync(user, "User");

    var token = await authHelpers.GenerateJWTToken(user); 

        return new RegisterUserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName
        };

        } catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create account");
            throw new ArgumentException(ex.Message, "Failed to register user");
        }


    }
}