using System.ComponentModel.DataAnnotations;
using BloggingAppV2.Helpers.CustomValidators;

namespace BloggingApp.Web.Models.DTOs;

public class CreateBlogRequest
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [ListOfTagsValidator]
    public List<string> Tags { get; set; }
    
}