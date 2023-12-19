using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.Repositories;
using BloggingAppV2.RepositoriesInterface;

namespace BloggingAppV2.Repositories;

public class UserPostLikeRepository : BaseRepository<UserPostLikes>, IUserPostLikeRepository
{
    public UserPostLikeRepository(ApplicationDbContext context) : base(context)
    { }
}