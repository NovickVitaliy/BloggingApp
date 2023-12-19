using BloggingApp.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BloggingAppV2.Components;

public class PersonalPostViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PostResponse? post)
    {
        return View(post);
    }
}