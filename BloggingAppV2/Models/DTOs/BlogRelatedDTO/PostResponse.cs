using System.Collections;

namespace BloggingApp.Web.Models.DTOs;

public class PostResponse
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IEnumerable<TagResponse> Tags { get; set; }
    public int Likes { get; set; }
}