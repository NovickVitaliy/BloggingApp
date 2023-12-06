using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.RepositoriesInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
public class UsersController : Controller
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper; 

    public UsersController(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> ShowUsers(string searchName = "")
    {
        ViewData["ActiveLink"] = "ShowUsers";
        if (!string.IsNullOrEmpty(searchName))
        {
            var users = await _repositoryManager.UserRepository.FindAll(false)
                .Include(u => u.Photo)
                .ToListAsync();

            var usersToReturn = users.Where(u => u.FullName.Contains(searchName, StringComparison.OrdinalIgnoreCase));

            return View(_mapper.Map<IEnumerable<UserDto>>(usersToReturn));
        }
        return View();
    }
}