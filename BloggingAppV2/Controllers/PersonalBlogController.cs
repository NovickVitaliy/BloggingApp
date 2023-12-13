using BloggingApp.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
public class PersonalBlogController : Controller
{
    public IActionResult MyBlog()
    {
        ViewData["ActiveLink"] = "MyBlog";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CreateBlogRequest createBlogRequest)
    {
        if (!ModelState.IsValid)
        {
            return View("MyBlog", createBlogRequest);
        }
        

        return Ok();
    }
}