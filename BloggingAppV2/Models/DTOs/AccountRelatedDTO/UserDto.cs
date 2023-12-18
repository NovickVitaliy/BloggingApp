using BloggingApp.Web.Models.Main;

namespace BloggingApp.Web.Models.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? Description { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public DateOnly? CreatedAt { get; set; }
    public PhotoDto? Photo { get; set; }
    public CountryDto? Country { get; set; } 
    public List<PostResponse> Posts { get; set; }
}