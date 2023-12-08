using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Models.DTOs;

public class AddFriendRequest
{
    public User FromUser { get; set; }
    public User ToUser { get; set; }
}