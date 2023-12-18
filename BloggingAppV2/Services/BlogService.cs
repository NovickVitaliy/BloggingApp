using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.Models.Main.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task EditPost(EditPostRequest editPostRequest)
    {
        Post? postToEdit = _repositoryManager.PostRepository.FindByCondition(post => post.Id == editPostRequest.Id,
                true)
            .Include(post => post.Tags)
            .FirstOrDefault();

        if (postToEdit == null)
        {
            throw new ArgumentNullException();
        }

        var tags = await _tagsService.GetTags(editPostRequest.Tags, true);

        postToEdit = _mapper.Map(editPostRequest, postToEdit);

        postToEdit.Tags = tags;
        
        foreach (var tag in tags)
        {
            if(!tag.Posts.Contains(postToEdit))
                tag.Posts.Add(postToEdit);
        }

        await _repositoryManager.Save();
    }

    public async Task DeletePost(Guid postId)
    {
        var postToDelete = _repositoryManager.PostRepository.FindByCondition(post => post.Id == postId ,true)
            .First();

        _repositoryManager.PostRepository.Delete(postToDelete);

        await _repositoryManager.Save();
    }

    public Task LikePost(Guid postId)
    {
        throw new NotImplementedException();
    }
}