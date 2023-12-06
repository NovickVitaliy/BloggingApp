using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class FriendsController : Controller
{
    private readonly IFriendsService _friendsService;
    private readonly IRepositoryManager _repositoryManager;
    
    public FriendsController(IFriendsService friendsService)
    {
        _friendsService = friendsService;
    }

    [HttpPost]
    [Route("{userId}")]
    public async Task<IActionResult> AddFriend(Guid userId)
    {
        string? currentUserEmail = User.Identity.Name;

        var currentUser = _repositoryManager.UserRepository
            .FindByCondition(u => u.Email == currentUserEmail, true)
            .First();

        var userToSendFriendRequest = _repositoryManager.UserRepository
            .FindByCondition(u => u.Id == userId, true)
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