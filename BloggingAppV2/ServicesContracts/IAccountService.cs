using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.General.AccountRelatedModels;

namespace BloggingApp.Web.ServicesContracts;

public interface IAccountService
{
    Task<RegisterResult> RegisterAsync(RegisterDto? registerDto);
    Task<LoginResult> LoginAsync(LoginDto loginDto);
    Task LogoutAsync();
}