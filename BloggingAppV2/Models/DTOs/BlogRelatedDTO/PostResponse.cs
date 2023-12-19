namespace BloggingApp.Web.Models.DTOs;

public class PostResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public IEnumerable<TagResponse> Tags { get; set; }
    public int Likes { get; set; }
    public bool IsLiked { get; set; }
}