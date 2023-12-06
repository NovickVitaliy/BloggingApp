using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.DTOs;

namespace BloggingApp.Web.Models.General.AccountRelatedModels;

public class LoginResult
{
    public LoginStatus? Status { get; set; }
    public string[]? Errors { get; set; }
}