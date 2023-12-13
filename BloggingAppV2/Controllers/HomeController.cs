using BloggingApp.Web.ServicesContracts;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILoggerManager _loggerManager;

    public HomeController(ILoggerManager loggerManager)
    {
        _loggerManager = loggerManager;
    }

    [Route("/")]
    [Route("[action]")]
    public IActionResult Home()
    {
        _loggerManager.LogError("AHAHHAHAAH LOSER");
        
        return View();
    }
}