using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class FriendsController : Controller
{
    private readonly IFriendsService _friendsService;
    private readonly IRepositoryManager _repositoryManager;
    
    public FriendsController(IFriendsService friendsService, IRepositoryManager repositoryManager)
    {
        _friendsService = friendsService;
        _repositoryManager = repositoryManager;
    }

    [HttpGet]
    [Route("{userEmail}")]
    public async Task<IActionResult> AddFriend(string userEmail)
    {
        string? currentUserEmail = User.Identity.Name;

        var currentUser = _repositoryManager.UserRepository
            .FindByCondition(u => u.Email == currentUserEmail, true)
            .Include(u => u.MailBox)
            .Include(u => u.Photo)
            .First();

        var userToSendFriendRequest = _repositoryManager.UserRepository
            .FindByCondition(u => u.Email == userEmail, true)
            .Include(u => u.MailBox)
            .Include(u=> u.Photo)
            .First();

        if (userToSendFriendRequest == null)
        {
            return NotFound();
        }

        var isAddFriendSuccess = await _friendsService.AddFriend(new AddFriendRequest()
        {
            FromUser = currentUser,
            ToUser = userToSendFriendRequest
        });

        if (isAddFriendSuccess)
        {
            await _repositoryManager.Save();
            return Ok();
        }

        return Problem("Sending friend request was not successfull");
    }
}