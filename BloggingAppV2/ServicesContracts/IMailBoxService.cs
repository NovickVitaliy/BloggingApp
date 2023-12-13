using BloggingApp.Web.Models.DTOs;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.ServicesContracts;

public interface IMailBoxService
{
    Task CreateMailBox(User user);
    Task<bool> SendMessage(SendMessageRequest sendMessageRequest);
}