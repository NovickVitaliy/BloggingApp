using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.Models.DTOs.ForMyPageDTO;
using BloggingAppV2.Models.Main.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize]
public class ForMyPageController : Controller
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly IBlogService _blogService;

    public ForMyPageController(IRepositoryManager repository,
        IMapper mapper,
        IMemoryCache cache, IBlogService blogService)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
        _blogService = blogService;
    }

    public async Task<IActionResult> ForMyPage(ForMyPageResponse? forMyPageResponse)
    {
        forMyPageResponse ??= new ForMyPageResponse();

        ViewData["ActivePage"] = forMyPageResponse.CurrentPage;

        var posts = _repository.PostRepository.FindAll(false)
            .Include(p => p.Tags)
            .ToList();
        await SetCachedPosts(posts);

        var postsToShow = _mapper.Map<List<PostResponse>>(posts
            .Skip(forMyPageResponse.AmountPerPage * (forMyPageResponse.CurrentPage - 1))
            .Take(forMyPageResponse.AmountPerPage)
            .ToList());

        forMyPageResponse.Posts = postsToShow;

        forMyPageResponse.NumberOfPages = (posts.Count / forMyPageResponse.AmountPerPage) + 1;

        return View(forMyPageResponse);
    }

    public async Task<IActionResult> NextPage(ForMyPageResponse forMyPageResponse, int page)
    {
        forMyPageResponse.CurrentPage = page;
        return RedirectToAction("ForMyPage", new RouteValueDictionary(forMyPageResponse));
    }

    public async Task<IActionResult> Like(ForMyPageResponse forMyPageResponse, Guid postId)
    {
        string currentUserEmail = User.Identity.Name;

        User currentUser = _repository.UserRepository.FindByCondition(u => u.Email == currentUserEmail, true)
            .Include(u => u.Posts)
            .ThenInclude(p => p.Tags)
            .Include(u => u.LikedPosts)
            .First();

        if (!(currentUser.LikedPosts.Count(e => e.PostId == postId) > 0))
        {
            await _blogService.LikePost(postId);
            currentUser.LikedPosts.Add(new UserPostLikes() { UserId = currentUser.Id, PostId = postId });
        }
        else
        {
            await _blogService.UnlikePost(postId);
            var postToUnlike = currentUser.LikedPosts.First(e => e.PostId == postId && e.UserId == currentUser.Id);
            _repository.UserPostLikeRepository.Delete(postToUnlike);
        }

        await _repository.Save();

        return RedirectToAction("ForMyPage", new RouteValueDictionary(forMyPageResponse));
    }

    private async Task<List<Post>> GetCachedPosts()
    {
        return (List<Post>)_cache.Get("cachedPosts")!;
    }

    private async Task SetCachedPosts(List<Post> posts)
    {
        _cache.Set("cachedPosts", posts, new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
    }
}