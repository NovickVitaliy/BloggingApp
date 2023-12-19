using BloggingApp.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BloggingAppV2.Components.ForMyPageComponents;

public class ForMyPagePostViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PostResponse post)
    {
        return View(post);
    }
}