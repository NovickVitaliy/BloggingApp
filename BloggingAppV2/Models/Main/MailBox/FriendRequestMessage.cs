using BloggingApp.Web.Models.DTOs;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Models.Main;

public class FriendRequestMessage : MailBoxMessage
{
    public Guid FromUserId { get; set; }
    public User FromUser { get; set; }
    
    public Guid ToUserId { get; set; }
    public User ToUser { get; set; }
    public bool Accepted { get; set; }
}