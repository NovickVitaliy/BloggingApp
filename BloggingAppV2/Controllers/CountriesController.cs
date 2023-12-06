using BloggingApp.Web.Models.Main;
using BloggingApp.Web.ServicesContracts;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
public class CountriesController : Controller
{
    private readonly ICountriesService _countriesService;
    public CountriesController(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountryByName(string? name)
    {
        if (name == null)
        {
            return NoContent();
        }

        var country = await _countriesService.GetByName(name);

        if (country == null)
        {
            return BadRequest();
        }

        return Ok(country);
    }
}