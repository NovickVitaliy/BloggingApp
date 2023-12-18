using BloggingAppV2.Helpers.CustomValidators;

namespace BloggingApp.Web.Models.DTOs;

public class EditPostRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<TagRequest> Tags { get; set; }
}