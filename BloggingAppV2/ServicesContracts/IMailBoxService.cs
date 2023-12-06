using BloggingApp.Web.Models.DTOs;

namespace BloggingApp.Web.ServicesContracts;

public interface IMailBoxService
{
    Task<bool> SendMessage(SendMessageRequest sendMessageRequest);
}