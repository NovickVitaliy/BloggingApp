using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.RepositoriesInterface;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private readonly Lazy<IUserRepository> _lazyUserReposotiry;
    private readonly Lazy<IPhotoRepository> _lazyPhotoRepository;
    private readonly Lazy<ICountriesRepository> _lazyCountriesRepository;

    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        _lazyPhotoRepository = new Lazy<IPhotoRepository>(() => new PhotoRepository(context));
        _lazyUserReposotiry = new Lazy<IUserRepository>(() => new UserRepository(context));
        _lazyCountriesRepository = new Lazy<ICountriesRepository>(() => new CountriesRepository(context));
    }
    public IUserRepository UserRepository => _lazyUserReposotiry.Value;
    public IPhotoRepository PhotoRepository => _lazyPhotoRepository.Value;
    public ICountriesRepository CountriesRepository => _lazyCountriesRepository.Value;

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}