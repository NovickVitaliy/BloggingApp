using BloggingApp.Web.DataBase.EntityConfigurations;
using BloggingApp.Web.Models.Main;
using BloggingApp.Web.Services;
using BloggingAppV2.Models.Main.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Web.DataBase;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    public ApplicationDbContext()
    { }
    
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Country> Countries { get; set; }
    //public DbSet<MailBox> MailBoxes { get; set; }
    //public DbSet<MailBoxMessage> MailBoxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PhotoConfiguration());
        builder.ApplyConfiguration(new CountryConfiguration());
        // builder.Entity<MailBoxMessage>().UseTpcMappingStrategy();
        // builder.Entity<SystemMessage>().ToTable("SystemMessages");
        // builder.Entity<FriendRequestMessage>().ToTable("FriendRequestMessages");
        //
        // builder.Entity<Country>()
        //     .HasMany(c => c.Users)
        //     .WithOne(u => u.Country)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // builder.Entity<User>()
        //     .HasOne(p => p.Photo)
        //     .WithOne(u => u.User)
        //     .HasForeignKey<Photo>(p => p.Id);
        //
        // builder.Entity<UserFriendship>(b =>
        // {
        //     b.HasKey(x => new { x.UserId, x.UserFriendId });
        //
        //     b.HasOne(x => x.User)
        //         .WithMany(x => x.Friends)
        //         .HasForeignKey(x => x.UserId)
        //         .OnDelete(DeleteBehavior.Restrict);
        // });
        //
        // builder.Entity<User>()
        //     .HasOne(u => u.MailBox)
        //     .WithOne(mailBox => mailBox.User)
        //     .HasForeignKey<MailBox>(box => box.Id);
        //
        // builder.Entity<MailBox>()
        //     .HasMany(m => m.Messages)
        //     .WithOne(message => message.MailBox)
        //     .HasForeignKey(message => message.MailBoxId);
        //
        base.OnModelCreating(builder);
    }
}