using System.ComponentModel.DataAnnotations;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.ServicesContracts;

namespace BloggingAppV2.Helpers.CustomValidators;

public class ListOfTagsValidator : ValidationAttribute
{
    private string Error => "All tags should start with the character #"; 
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        object obj = validationContext.ObjectInstance;
        IEnumerable<TagRequest>? tags = null;
        if(obj is CreatePostRequest createPostRequest)
        {
            tags = createPostRequest.Tags;
        }else if (obj is EditPostRequest editPostRequest)
        {
            tags = editPostRequest.Tags;
        }
        else
        {
            throw new InvalidOperationException($"Cannot get the tag property from object of type: {validationContext.ObjectType}");
        }
        if (tags == null || !tags.Any())
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