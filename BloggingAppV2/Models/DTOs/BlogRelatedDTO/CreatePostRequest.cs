using System.ComponentModel.DataAnnotations;
using BloggingApp.Web.Services;
using BloggingAppV2.Helpers.CustomValidators;

namespace BloggingApp.Web.Models.DTOs;

public class CreatePostRequest
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [ListOfTagsValidator]
    public List<TagRequest> Tags { get; set; }
    
}