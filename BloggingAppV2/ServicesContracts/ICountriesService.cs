using BloggingApp.Web.Models.Main;

namespace BloggingApp.Web.ServicesContracts;

public interface ICountriesService
{
    Task<Country?> GetByName(string name);
}