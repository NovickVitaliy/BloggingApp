using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;

namespace BloggingApp.Web.ServicesContracts;

public interface ITagsService
{
    Task<List<Tag>> GetTags(IEnumerable<TagRequest> tagRequests, bool isEditing = false);
}