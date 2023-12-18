using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.Services;

public class TagsService : ITagsService
{
    private readonly IRepositoryManager _repositoryManager;
    

    public TagsService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<List<Tag>> GetTags(IEnumerable<TagRequest> tagRequests, bool isEditing = false)
    {
        var tags = _repositoryManager.TagRepository
            .FindByCondition(tag => tagRequests.Select(request => request.Name).Contains(tag.Name), true)
            .Include(tag => tag.Posts)
            .ToList();

        if (tags.Count() != tagRequests.Count())
        {
            IEnumerable<string> existedTagsName = tags.Select(tag => tag.Name);
            foreach (var tagRequest in tagRequests)
            {
                if (!existedTagsName.Contains(tagRequest.Name))
                {
                    var newTag = await CreateTag(tagRequest.Name);
                    tags.Add(newTag);
                    if (isEditing)
                    {
                        await IncrementUsagesOfTag(newTag);
                    }
                }
            }
        }

        if (!isEditing)
        {
            foreach (var tag in tags)
            {
                await IncrementUsagesOfTag(tag);
            }
        }
        return tags;
    }

    private async Task<Tag> CreateTag(string name)
    {
        var tag = new Tag() { Name = name, Posts = new List<Post>()};
        _repositoryManager.TagRepository.Create(tag);
        return tag;
    }

    private async Task IncrementUsagesOfTag(Tag tags)
    {
        tags.Usages++;
    }
}