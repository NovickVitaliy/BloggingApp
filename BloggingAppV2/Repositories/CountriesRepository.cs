using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.RepositoriesInterface;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Repositories;

public class CountriesRepository : BaseRepository<Country>, ICountriesRepository
{
    public CountriesRepository(ApplicationDbContext context) 
        : base(context)
    {
    }
}