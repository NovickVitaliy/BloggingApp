using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Identity;
using BloggingApp.Web.ServicesContracts;

namespace BloggingApp.Web.Services;

public class FriendsService : IFriendsService
{
    private readonly IMailBoxService _mailBoxService;

    public FriendsService(IMailBoxService mailBoxService)
    {
        _mailBoxService = mailBoxService;
    }

    public async Task<bool> AddFriend(AddFriendRequest? addFriendRequest)
    {
        if (addFriendRequest == null)
            throw new NullReferenceException();

        User? requestUser = addFriendRequest.FromUser;
        
        if (requestUser == null)
            throw new NullReferenceException();

        User? receiverUser = addFriendRequest.ToUser;
        if (receiverUser == null)
            throw new NullReferenceException();

        bool success = await _mailBoxService.SendMessage(new SendMessageRequest()
        {
            Sender = requestUser,
            Receiver = receiverUser,
            MessageType = MessageType.FriendRequestMessage
        });

        return success;
    }

    public Task RemoveFriend(DeleteFriendRequest deleteFriendRequest)
    {
        throw new NotImplementedException();
    }
}