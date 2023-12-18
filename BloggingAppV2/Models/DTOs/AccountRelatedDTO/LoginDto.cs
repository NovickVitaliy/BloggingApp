using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Web.Models.DTOs;

public class LoginDto
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "{0} must be supplied")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} must be supplied")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}