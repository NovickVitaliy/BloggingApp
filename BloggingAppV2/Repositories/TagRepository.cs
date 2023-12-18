using BloggingApp.Web.DataBase;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.Repositories;
using BloggingAppV2.RepositoriesInterface;

namespace BloggingAppV2.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }
}