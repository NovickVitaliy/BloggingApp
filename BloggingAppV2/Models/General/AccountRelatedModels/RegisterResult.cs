using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.DTOs;

namespace BloggingApp.Web.Models.General.AccountRelatedModels;

public class RegisterResult
{
    public RegisterStatus? Status { get; set; }
    public string[]? Errors { get; set; }
}