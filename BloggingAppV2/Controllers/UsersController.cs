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
                .Where(e => EF.Functions.Like(e.FullName, $"%{searchName}%"))
                .ToListAsync();

            //var usersToReturn = users.Where(u => u.FullName.Contains(searchName, StringComparison.OrdinalIgnoreCase));

            return View(_mapper.Map<IEnumerable<UserDto>>(users));
        }
        return View();
    }

    public async Task<IActionResult> ProfileOfUser(Guid id)
    {
        var userToShow = _repositoryManager.UserRepository.FindByCondition(u => u.Id == id, false)
            .Include(u => u.Photo)
            .Include(u => u.Posts)
            .ThenInclude(p => p.Tags)
            .Include(u => u.Country)
            .First();

        UserDto userDto = _mapper.Map<UserDto>(userToShow);

        return View(userDto);
    }
}