namespace BloggingApp.Web.Models.Main.Blogs;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Usages { get; set; }
    public List<Post> Posts { get; set; }
    public List<PostTag> PostTags { get; set; }
}