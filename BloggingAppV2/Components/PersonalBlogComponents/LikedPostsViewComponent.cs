using BloggingApp.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BloggingAppV2.Components;

public class LikedPostsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<PostResponse> posts)
    {
        return View(posts);
    }
}