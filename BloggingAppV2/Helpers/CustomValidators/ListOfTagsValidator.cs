using System.ComponentModel.DataAnnotations;
using BloggingApp.Web.Models.DTOs;

namespace BloggingAppV2.Helpers.CustomValidators;

public class ListOfTagsValidator : ValidationAttribute
{
    private string Error => "All tags should start with the character #"; 
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        CreatePostRequest createPostRequest = (CreatePostRequest)validationContext.ObjectInstance;
        List<TagRequest>? tags = createPostRequest.Tags;
        if (tags == null || tags.Count == 0)
            return ValidationResult.Success;
        
        foreach (var tag in tags)
        {
            if (!tag.Name.StartsWith('#'))
            {
                return new ValidationResult(Error);
            }
        }
        
        return ValidationResult.Success;
    }
}