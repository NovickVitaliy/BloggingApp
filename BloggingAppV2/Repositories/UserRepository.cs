using BloggingApp.Web.DataBase;
using BloggingApp.Web.RepositoriesInterface;
using BloggingAppV2.Models.Main.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) 
        : base(context)
    { }
}