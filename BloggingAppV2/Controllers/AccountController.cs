using AutoMapper;
using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.Models.Main.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private readonly ICountriesService _countriesService;
    private readonly IMemoryCache _cache;
    private readonly IMailBoxService _mailBoxService;

    public AccountController(IAccountService accountService,
        IRepositoryManager repositoryManager,
        IMapper mapper,
        IPhotoService photoService,
        ICountriesService countriesService,
        IMemoryCache cache, 
        IMailBoxService mailBoxService)
    {
        _accountService = accountService;
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _photoService = photoService;
        _countriesService = countriesService;
        _cache = cache;
        _mailBoxService = mailBoxService;
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        ViewData["ActiveLink"] = "Register";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Registration", "Registration was not successfull");
            return View();
        }

        var registerResult = await _accountService.RegisterAsync(registerDto);

        if (registerResult.Status == RegisterStatus.Success)
        {
            await _mailBoxService.CreateMailBox(_repositoryManager.UserRepository
                .FindByCondition(u => u.Email == User.Identity.Name, true).Single());
            ViewData["ActiveLink"] = "none";
            return LocalRedirect("~/Account/ConfigureUserAccount");
        }

        foreach (var registerResultError in registerResult.Errors!)
        {
            ModelState.AddModelError("Register", registerResultError);
        }

        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Account()
    {
        var currentUserEmail = User.Identity!.Name;

        if (currentUserEmail == null)
        {
            return NotFound("User does not exist");
        }

        User? user = (User)_cache.Get(currentUserEmail)!;

        
        if (user == null)
        {
            user = _repositoryManager.UserRepository.FindByCondition(u => u.Email == currentUserEmail, false)
                .Include(u => u.Country)
                .Include(u => u.Photo)
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound("User was not found");
            }

            _cache.Set(currentUserEmail, user,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }

        ViewData["ActiveLink"] = "Account";
        var userDto = _mapper.Map<UserDto>(user);
        return View(userDto);
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        ViewData["ActiveLink"] = "Login";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Login", "Login was not successfull");
            return View();
        }

        var loginResult = await _accountService.LoginAsync(loginDto);

        if (loginResult.Status == LoginStatus.Success)
        {
            ViewData["ActiveLink"] = "none";
            return LocalRedirect("~/Home/Home");
        }

        foreach (var loginResultError in loginResult.Errors!)
        {
            ModelState.AddModelError("Login", loginResultError);
        }

        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return LocalRedirect("~/Home/Home");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ConfigureUserAccount()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ConfigureUserAccount(ConfigureUserDetailDto configureUserDetailDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        string? emailOfCurrentUser = User.Identity!.Name;

        User? currentUser = (User)_cache.Get(emailOfCurrentUser);

        if (currentUser == null)
        {
            currentUser = _repositoryManager.UserRepository
                .FindByCondition(u => u.Email == emailOfCurrentUser, true)
                .FirstOrDefault();

            if (currentUser == null)
            {
                return NotFound();
            }
        }


        
        var countryName = configureUserDetailDto.CountryName!.ToLower();
        var countryForUser = _repositoryManager.CountriesRepository
            .FindByCondition(c => c.Name == countryName, true).FirstOrDefault();
        
        if (countryForUser == null)
        {
            countryForUser = await _countriesService.GetByName(countryName);
            countryForUser!.Name = countryName.ToLower();
            _repositoryManager.CountriesRepository.Create(countryForUser!);
        }


        currentUser.Description = configureUserDetailDto.Description;
        currentUser.DateOfBirth = configureUserDetailDto.DateOfBirth;
        currentUser.Gender = configureUserDetailDto.Gender;
        currentUser.CountryId = countryForUser.Id;
        await _repositoryManager.Save();
        UpdateUserInCache(emailOfCurrentUser, currentUser);
        return LocalRedirect("~/Home/Home");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SetProfilePhoto(IFormFile file)
    {
        User? user = (User)_cache.Get(User.Identity.Name);

        if (user == null)
        {
            user = _repositoryManager.UserRepository.FindByCondition(
                    u => u.Email == User.Identity.Name, true)
                .Include(u => u.Photo)
                .FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
        }


        if (user.Photo != null)
        {
            await _photoService.DeletePhotoAsync(user.Photo.PublicId);
            _repositoryManager.PhotoRepository.Delete(user.Photo);
        }

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null)
        {
            return BadRequest(result.Error.Message);
        }

        var photo = new Photo()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId,
        };

        photo.UserId = user.Id;
        user.Photo = photo;
        _repositoryManager.PhotoRepository.Create(photo);
        await _repositoryManager.Save();
        UpdateUserInCache(User.Identity.Name, user);
        return LocalRedirect("~/Account/Account");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> EditProfile()
    {
        var currentUserEmail = User.Identity.Name;

        User? userToUpdate = _cache.Get<User>(currentUserEmail);

        if (userToUpdate == null)
        {
            userToUpdate = _repositoryManager.UserRepository
                .FindByCondition(u => u.Email == currentUserEmail, false)
                .Include(u => u.Country)
                .FirstOrDefault();
        }


        return View(_mapper.Map<EditProfileDto>(userToUpdate));
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditProfile(EditProfileDto editProfileDto)
    {
        var currentUserEmail = User.Identity.Name;

        User? userToUpdate = _cache.Get<User>(currentUserEmail);

        if (userToUpdate == null)
        {
            userToUpdate = _repositoryManager.UserRepository
                .FindByCondition(u => u.Email == currentUserEmail, true)
                .Include(u => u.Photo)
                .Include(u => u.Country)
                .FirstOrDefault();
        }


        string countryName = editProfileDto.CountryName.ToLower();
        
        var country = _repositoryManager.CountriesRepository.FindByCondition(c => c.Name == countryName, true)
            .FirstOrDefault();
        
        if (country == null)
        {
            country = await _countriesService.GetByName(countryName);
            country.Name = countryName.ToLower();
            _repositoryManager.CountriesRepository.Create(country);
        }

        _mapper.Map(editProfileDto, userToUpdate);
        country.Users.Add(userToUpdate);
        userToUpdate.Country = country;
        userToUpdate.CountryId = country.Id;
        await _repositoryManager.Save();
        UpdateUserInCache(currentUserEmail, userToUpdate);

        return LocalRedirect("~/Account/Account");
    }

    public async Task<JsonResult> IsCountryValid(string countryName)
    {
        var country = await _countriesService.GetByName(countryName);

        return Json(country != null);
    }

    public void UpdateUserInCache(string emailOfUser, User user)
    {
        _cache.Set(emailOfUser, user);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MailBox()
    {
        string? currentUserEmail = User.Identity.Name;

        var currentUser = (User)_cache.Get(currentUserEmail);

        if (currentUser == null || currentUser.MailBox == null 
                                || currentUser.MailBox.Messages == null)
        {
            currentUser = _repositoryManager.UserRepository
                .FindByCondition(u => u.Email == currentUserEmail, true)
                .Include(e => e.MailBox)
                .ThenInclude(e => e.Messages)
                .First();

            _repositoryManager.MailBoxRepository.FindByCondition(e => e.UserId == currentUser.Id, false);
            
            
            
            _cache.Set(currentUserEmail, currentUser);
        }

        return View(currentUser.MailBox);
    }
}