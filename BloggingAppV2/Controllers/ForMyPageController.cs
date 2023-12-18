using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
public class ForMyPageController : Controller
{
    public IActionResult ForMyPage()
    {
        return View();
    }
}