using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.Services;
using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.Repositories;
using BloggingAppV2.RepositoriesInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private readonly Lazy<IUserRepository> _lazyUserReposotiry;
    private readonly Lazy<IPhotoRepository> _lazyPhotoRepository;
    private readonly Lazy<ICountriesRepository> _lazyCountriesRepository;
    private readonly Lazy<IMailBoxRepository> _lazyMailBoxRepository;
    private readonly Lazy<IPostRepository> _lazyPostRepository;
    private readonly Lazy<ITagRepository> _lazyTagRepository;
    private readonly Lazy<IUserPostLikeRepository> _lazyPostLikeRepository;

    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        _lazyPhotoRepository = new Lazy<IPhotoRepository>(() => new PhotoRepository(context));
        _lazyUserReposotiry = new Lazy<IUserRepository>(() => new UserRepository(context));
        _lazyCountriesRepository = new Lazy<ICountriesRepository>(() => new CountriesRepository(context));
        _lazyMailBoxRepository = new Lazy<IMailBoxRepository>(() => new MailBoxRepository(context));
        _lazyPostRepository = new Lazy<IPostRepository>(() => new PostRepository(context));
        _lazyTagRepository = new Lazy<ITagRepository>(() => new TagRepository(context));
        _lazyPostLikeRepository = new Lazy<IUserPostLikeRepository>(() => new UserPostLikeRepository(context));
    }
    public IUserRepository UserRepository => _lazyUserReposotiry.Value;
    public IPhotoRepository PhotoRepository => _lazyPhotoRepository.Value;
    public ICountriesRepository CountriesRepository => _lazyCountriesRepository.Value;

    public IMailBoxRepository MailBoxRepository => _lazyMailBoxRepository.Value;
    public IPostRepository PostRepository => _lazyPostRepository.Value;
    public ITagRepository TagRepository => _lazyTagRepository.Value;
    public IUserPostLikeRepository UserPostLikeRepository => _lazyPostLikeRepository.Value;

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}