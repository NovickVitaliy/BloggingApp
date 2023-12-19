using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingAppV2.Models.DTOs.ForMyPageDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
public class ForMyPageController : Controller
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    public ForMyPageController(IRepositoryManager repository, 
        IMapper mapper, 
        IMemoryCache cache)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IActionResult> ForMyPage(ForMyPageResponse forMyPageResponse,int amountPerPage = 10, int page = 1)
    {
        if (forMyPageResponse == null)
        {
            forMyPageResponse = new ForMyPageResponse();
        }

        forMyPageResponse.CurrentPage = page - 1;
        ViewData["ActivePage"] = page;
        
        var posts = await GetCachedPosts();
        if (posts == null)
        {
            posts = _repository.PostRepository.FindAll(false)
                .Include(p => p.Tags)
                .ToList();
            await SetCachedPosts(posts);
        }

        var postsToShow = _mapper.Map<List<PostResponse>>(posts.Skip(amountPerPage * forMyPageResponse.CurrentPage)
            .Take(forMyPageResponse.AmountPerPage)
            .ToList());

        forMyPageResponse.Posts = postsToShow;

        forMyPageResponse.NumberOfPages = (posts.Count / forMyPageResponse.AmountPerPage) + 1;

        return View(forMyPageResponse);
    }
    
    public async Task<IActionResult> NextPage(int pageToGo, int amountPerPage)
    {
        
        return RedirectToAction("ForMyPage", new {amountPerPage = amountPerPage , page = pageToGo });
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