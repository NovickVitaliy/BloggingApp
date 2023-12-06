using BloggingApp.Web.ServicesContracts;

namespace BloggingApp.Web.RepositoriesInterface;

public interface IRepositoryManager
{
    IUserRepository UserRepository { get; }
    IPhotoRepository PhotoRepository { get; }
    ICountriesRepository CountriesRepository { get; }
    IMailBoxRepository MailBoxRepository { get; }
    
    Task Save();
}