using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Web.Models.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "{0} must be supplied")]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "{0} must be supplied")]
    [Display(Name = "Full Name")]
    [DataType(DataType.Text)]
    public string? FullName { get; set; }
    
    [Required(ErrorMessage = "{0} must be supplied")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "{0} must be supplied")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "{0} and {1} must match")]
    public string? ConfirmPassword { get; set; }
}