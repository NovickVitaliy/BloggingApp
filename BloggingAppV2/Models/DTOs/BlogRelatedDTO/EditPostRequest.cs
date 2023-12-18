namespace BloggingApp.Web.Models.DTOs;

public class EditPostRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<TagRequest> Tags { get; set; }
}