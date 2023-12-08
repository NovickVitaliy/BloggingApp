using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingApp.Web.DataBase.EntityConfigurations;

public class MailBoxMessagesConfiguration<T> : IEntityTypeConfiguration<MailBoxMessage> where T : class 
{
    public virtual void Configure(EntityTypeBuilder<MailBoxMessage> builder)
    {
        builder.HasOne(e => e.MailBox)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => e.MailBoxId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}