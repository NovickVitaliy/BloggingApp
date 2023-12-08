using BloggingApp.Web.Models.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingApp.Web.DataBase.EntityConfigurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.HasOne(e => e.User)
            .WithOne(u => u.Photo)
            .HasForeignKey<Photo>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}