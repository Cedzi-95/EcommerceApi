using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    public Task<RegisterUserResponse> RegisterUserAsync(RegisterUserDto request);
    public Task<LoginResponseDto> LoginUserAsync(LoginRequestDto request);
    public Task<IEnumerable<UserEntity>> GetAllAsync();
    public Task<UserEntity> GetByIdAsync(string userId);
    public Task<UserEntity> DeleteAsync(string userId);
}

public class UserService : IUserService
{
    public UserManager<UserEntity> userManager;
    public SignInManager<UserEntity> signInManager;

    public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    public async Task<UserEntity> DeleteAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId) ?? throw new ArgumentException("user not found");
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

    public async Task<UserEntity> GetByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId) ?? throw new ArgumentException("User not found");
    }

    public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user == null)
        {
            throw new ArgumentException("user not found");
        }
        var result = await signInManager.PasswordSignInAsync(
            user.UserName!,
            request.Password!,
            false,
            false
        );
        if (result.Succeeded)
        {
            return new LoginResponseDto()
            {
                Email = user.Email,
                Username = user.UserName
            };
        }

        return null!;

        
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

        return new RegisterUserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName
        };


    }
}