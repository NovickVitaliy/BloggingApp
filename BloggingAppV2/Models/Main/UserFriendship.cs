using BloggingApp.Web.Models.Identity;

namespace BloggingApp.Web.Models.Main;

public class UserFriendship
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid UserFriendId { get; set; }
    public User UserFriend { get; set; }
}