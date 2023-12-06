namespace BloggingApp.Web.RepositoriesInterface;

public interface IRepositoryManager
{
    IUserRepository UserRepository { get; }
    IPhotoRepository PhotoRepository { get; }
    ICountriesRepository CountriesRepository { get; }
    Task Save();
}