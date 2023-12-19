using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.RepositoriesInterface;

namespace BloggingApp.Web.RepositoriesInterface;

public interface IRepositoryManager
{
    IUserRepository UserRepository { get; }
    IPhotoRepository PhotoRepository { get; }
    ICountriesRepository CountriesRepository { get; }
    IMailBoxRepository MailBoxRepository { get; }
    IPostRepository PostRepository { get; }
    ITagRepository TagRepository { get; }
    IUserPostLikeRepository UserPostLikeRepository { get; }
    
    Task Save();
}