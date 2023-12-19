using System.ComponentModel.DataAnnotations;
using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.DTOs;

namespace BloggingAppV2.Models.DTOs.ForMyPageDTO;

public class ForMyPageResponse
{
    public PostOption PostOption { get; set; } = PostOption.AllRecent;

    [Range(minimum: 1, maximum: 20, ErrorMessage = "Provide a valid number of posts(1-20)")]
    public int AmountPerPage { get; set; } = 10;

    public int CurrentPage { get; set; } = 1;
    public int NumberOfPages { get; set; }
    public List<PostResponse> Posts { get; set; }
}