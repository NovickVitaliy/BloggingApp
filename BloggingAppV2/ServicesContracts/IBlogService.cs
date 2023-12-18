using BloggingApp.Web.Models.DTOs;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.ServicesContracts;

public interface IBlogService
{
    Task CreatePost(User userId, CreatePostRequest createPostRequest);
}