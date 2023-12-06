using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.Services;
using BloggingApp.Web.ServicesContracts;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private readonly Lazy<IUserRepository> _lazyUserReposotiry;
    private readonly Lazy<IPhotoRepository> _lazyPhotoRepository;
    private readonly Lazy<ICountriesRepository> _lazyCountriesRepository;
    private readonly Lazy<IMailBoxRepository> _lazyMailBoxRepository;

    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        _lazyPhotoRepository = new Lazy<IPhotoRepository>(() => new PhotoRepository(context));
        _lazyUserReposotiry = new Lazy<IUserRepository>(() => new UserRepository(context));
        _lazyCountriesRepository = new Lazy<ICountriesRepository>(() => new CountriesRepository(context));
        _lazyMailBoxRepository = new Lazy<IMailBoxRepository>(() => new MailBoxRepository(context));
    }
    public IUserRepository UserRepository => _lazyUserReposotiry.Value;
    public IPhotoRepository PhotoRepository => _lazyPhotoRepository.Value;
    public ICountriesRepository CountriesRepository => _lazyCountriesRepository.Value;

    public IMailBoxRepository MailBoxRepository => _lazyMailBoxRepository.Value;

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}