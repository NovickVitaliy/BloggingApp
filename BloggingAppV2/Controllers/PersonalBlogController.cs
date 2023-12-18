using AutoMapper;
using BloggingApp.Web.Models.DTOs;
using BloggingApp.Web.RepositoriesInterface;
using BloggingApp.Web.ServicesContracts;
using BloggingAppV2.Models.Main.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Controllers;

[Route("[controller]/[action]")]
[Authorize]
public class PersonalBlogController : Controller
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IBlogService _blogService;
    private readonly IMemoryCache _cache;
    public PersonalBlogController(IRepositoryManager repositoryManager, 
        IMapper mapper, 
        IBlogService blogService, 
        IMemoryCache cache)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _blogService = blogService;
        _cache = cache;
    }

    [HttpGet(Name = "MyBlog")]
    public IActionResult MyBlog()
    {
        ViewData["ActiveLink"] = "MyBlog";
        string email = User.Identity.Name;

        var posts = _repositoryManager.UserRepository.FindByCondition(user => user.Email == email, false)
            .Include(u => u.Posts)
            .ThenInclude(p => p.Tags)
            .First()
            .Posts;

        var postsResponses = _mapper.Map<List<PostResponse>>(posts);
        
        return View(postsResponses);
    }

    [HttpGet]
    public async Task<IActionResult> CreatePost()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostRequest createPostRequest)
    {
        if (!ModelState.IsValid)
        {
            return View("CreatePost", createPostRequest);
        }

        var currentUser = GetCurrentUser();

        await _blogService.CreatePost(await currentUser, createPostRequest);

        return LocalRedirect("~/PersonalBlog/MyBlog");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> EditPost(Guid id)
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditPost(EditPostRequest editPostRequest)
    {

        return LocalRedirect("~/PersonalBlog/MyBlog");
    }

    private async Task<User> GetCurrentUser()
    {
        string email = User.Identity.Name;

        User currentUser = (User)_cache.Get(email);
        if (currentUser == null)
        {
            currentUser = _repositoryManager.UserRepository.FindByCondition(e => e.Email == email, true).First();
            _cache.Set(email, currentUser);
        }

        return currentUser;
    }
}