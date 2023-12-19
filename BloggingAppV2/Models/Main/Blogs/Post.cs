using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Models.Main.Blogs;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Tag> Tags { get; set; }
    public int Likes { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public List<PostTag> PostTags { get; set; }
    public List<UserPostLikes> LikedByUsers { get; set; } = new();
}