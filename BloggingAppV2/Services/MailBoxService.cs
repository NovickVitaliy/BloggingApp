using AutoMapper;
using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Services;

public class MailBoxService : IMailBoxService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public MailBoxService(IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task CreateMailBox(User userToCreateMailBox)
    {
        MailBox mailBox = new MailBox()
        {
            User = userToCreateMailBox,
            UserId = userToCreateMailBox.Id
        };

        userToCreateMailBox.MailBox = mailBox;

        await _repositoryManager.Save();
    }

    public Task<bool> SendMessage(SendMessageRequest sendMessageRequest)
    {
        MailBoxMessage message = null;
        
        switch (sendMessageRequest.MessageType)
        {
            case MessageType.SystemMessage:

                break;
            case MessageType.FriendRequestMessage:
                message = new FriendRequestMessage()
                {
                    FromUser = sendMessageRequest.Sender,
                    FromUserId = sendMessageRequest.Sender.Id,
                    ToUser = sendMessageRequest.Receiver,
                    ToUserId = sendMessageRequest.Receiver.Id,
                    Accepted = false,
                    SentAt = DateTime.UtcNow,
                    Title = "Friend Request",
                    Description = $"{sendMessageRequest.Sender.FullName} wants to be friend with you!",
                };
                break;
            default :
                throw new ArgumentNullException();
        }

        if (message != null)
        {
            message.MailBoxId = sendMessageRequest.Receiver.MailBox.Id;
            sendMessageRequest.Receiver.MailBox.Messages.Add(message);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}