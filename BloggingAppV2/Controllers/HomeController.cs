using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    [Route("/")]
    [Route("[action]")]
    public IActionResult Home()
    {
        return View();
    }
}