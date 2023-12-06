using BloggingApp.Web.Models.DTOs;

namespace BloggingApp.Web.ServicesContracts;

public interface IFriendsService
{
    Task<bool> AddFriend(AddFriendRequest addFriendRequest);
    Task RemoveFriend(DeleteFriendRequest deleteFriendRequest);
}