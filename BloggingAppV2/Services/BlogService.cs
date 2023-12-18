using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Services;

public class BlogService : IBlogService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ITagsService _tagsService;

    public BlogService(IRepositoryManager repositoryManager, 
        IMapper mapper, 
        ITagsService tagsService)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _tagsService = tagsService;
    }


    public async Task CreatePost(User user, CreatePostRequest createPostRequest)
    {
        Post post = _mapper.Map<Post>(createPostRequest);
        post.Tags = new List<Tag>();

        post.UserId = user.Id;

        var tags = (await _tagsService.GetTags(createPostRequest.Tags)).ToList();
        
        foreach (var tag in tags)
        {
            tag.Posts.Add(post);
        }

        post.Tags = tags;
        user.Posts.Add(post);
        
        
        await _repositoryManager.Save();
    }
}