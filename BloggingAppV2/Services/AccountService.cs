using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.General.AccountRelatedModels;
using BloggingApp.Web.Models.Identity;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.ServicesContracts;
using Microsoft.AspNetCore.Identity;

namespace BloggingApp.Web.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IPhotoService _photoService;

    public AccountService(UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IPhotoService photoService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _photoService = photoService;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterDto? registerDto)
    {
        if (registerDto == null)
        {
            return new RegisterResult() { Status = RegisterStatus.Failed };
        }
        User newUser = new User()
        {
            FullName = registerDto.FullName,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        IdentityResult result = await _userManager.CreateAsync(newUser, registerDto.Password!);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(newUser, isPersistent: false);
            return new RegisterResult() { Status = RegisterStatus.Success};
        }
        
        return new RegisterResult
        {
            Status = RegisterStatus.Failed,
            Errors = result.Errors.Select(err => err.Description).ToArray()
        };
    }

    public async Task<LoginResult> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return new LoginResult()
            {
                Status = LoginStatus.Failed,
                Errors = new []{"Such user does not exist"}
            };
        }

        var passwordSignInAsync = await _signInManager.PasswordSignInAsync(user, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

        if (passwordSignInAsync.Succeeded)
        {
            return new LoginResult()
            {
                Status = LoginStatus.Success
            };
        }

        return new LoginResult()
        {
            Status = LoginStatus.Failed,
            Errors = new []{"Login was not successfull"}
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}