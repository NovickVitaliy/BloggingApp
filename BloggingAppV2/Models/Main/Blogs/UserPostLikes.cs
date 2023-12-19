using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Models.Main.Blogs;

public class UserPostLikes
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}