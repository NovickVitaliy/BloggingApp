using BloggingApp.Web.Models.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingApp.Web.DataBase.EntityConfigurations;

public class MailBoxConfiguration : IEntityTypeConfiguration<MailBox>
{
    public void Configure(EntityTypeBuilder<MailBox> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
            .WithOne(e => e.MailBox)
            .HasForeignKey<MailBox>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}