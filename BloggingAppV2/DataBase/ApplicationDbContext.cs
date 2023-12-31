using BloggingApp.Web.DataBase.EntityConfigurations;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.Models.Main.Blogs;
using BloggingApp.Web.Services;
using BloggingAppV2.Models.Main.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.DataBase;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    public DbSet<Photo> Photos { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<MailBox> MailBoxes { get; set; }
    public DbSet<MailBoxMessage> MailBoxMessages { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<UserPostLikes> UserPostLikes { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<SystemMessage>().ToTable("SystemMessages");
        builder.Entity<FriendRequestMessage>().ToTable("FriendRequestsMessages");
        builder.ApplyConfiguration(new PhotoConfiguration());
        builder.ApplyConfiguration(new CountryConfiguration());
        builder.ApplyConfiguration(new MailBoxConfiguration());
        builder.ApplyConfiguration(new PostConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new PostTagConfiguration());
        builder.ApplyConfiguration(new UserPostLikesConfiguration());
    }
}