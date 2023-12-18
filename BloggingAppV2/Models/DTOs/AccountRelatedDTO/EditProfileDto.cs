using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Models.DTOs;

public class EditProfileDto
{
    [Required]
    public string? FullName { get; set; }
    public string? Gender { get; set; } 
    public string? Description { get; set; }
    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }
    [Remote(controller:"Account", action:"IsCountryValid", ErrorMessage = "Country name must be valid")]
    public string? CountryName { get; set; }
}