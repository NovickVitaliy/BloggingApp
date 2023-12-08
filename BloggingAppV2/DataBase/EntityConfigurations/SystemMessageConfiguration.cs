using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingApp.Web.DataBase.EntityConfigurations;

public class SystemMessageConfiguration : MailBoxMessagesConfiguration<SystemMessageConfiguration>
{
    public override void Configure(EntityTypeBuilder<MailBoxMessage> builder)
    {
        base.Configure(builder);

        builder.ToTable("SystemMessages");
    }
}