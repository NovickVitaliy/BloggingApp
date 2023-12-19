using BloggingApp.Web.Models.Main.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingApp.Web.DataBase.EntityConfigurations;

public class UserPostLikesConfiguration : IEntityTypeConfiguration<UserPostLikes>
{
    public void Configure(EntityTypeBuilder<UserPostLikes> builder)
    {
        builder.HasKey(e => new {e.UserId, e.PostId});

        builder.HasOne(e => e.Post)
            .WithMany(p => p.LikedByUsers)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.User)
            .WithMany(e => e.LikedPosts)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}