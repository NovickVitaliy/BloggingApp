using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.Repositories;
using BloggingAppV2.RepositoriesInterface;

namespace BloggingAppV2.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {}
}