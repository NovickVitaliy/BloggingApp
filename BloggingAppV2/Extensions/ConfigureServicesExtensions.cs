using BloggingApp.Web.DataBase;
using BloggingApp.Web.Services;
using BloggingApp.Web.ServicesContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloggingApp.Web.Configurations;
using BloggingApp.Web.Repositories;
using BloggingApp.Web.RepositoriesInterface;
using BloggingAppV2.Models.Main.Identity;
using NLog;

namespace BloggingApp.Web.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddScoped<ILoggerManager, LoggerManager>();
        services.AddScoped<IAccountService, AccountService>();
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), 
            "/nlog.development.config"));
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IFriendsService, FriendsService>();
        services.AddScoped<IMailBoxService, MailBoxService>();
        services.AddHttpClient();
        services.AddMemoryCache();
        services.AddResponseCaching();  
        services.AddScoped<ICountriesService, CountriesService>();
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, IdentityRole<Guid>, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<IdentityRole<Guid>, ApplicationDbContext, Guid>>();


        return services;
    }
}