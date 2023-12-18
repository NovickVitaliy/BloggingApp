using System.ComponentModel.DataAnnotations;
using BloggingApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Models.DTOs;

public class ConfigureUserDetailDto
{
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? Gender { get; set; }
    
    [Display(Name = "Date Of Birth")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "{0} must be supplied")]
    public DateOnly? DateOfBirth { get; set; }
    
    [Required]
    [Remote(controller:"Account", action:"IsCountryValid", ErrorMessage = "Country name must be valid")]
    public string? CountryName { get; set; }
}