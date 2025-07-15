using Microsoft.AspNetCore.Identity;

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
    public Task<UserEntity> DeleteAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity> GetByIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> LoginUserAsync(LoginRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<RegisterUserResponse> RegisterUserAsync(RegisterUserDto request)
    {
        throw new NotImplementedException();
    }
}