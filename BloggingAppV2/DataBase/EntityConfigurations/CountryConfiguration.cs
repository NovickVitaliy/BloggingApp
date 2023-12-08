using BloggingApp.Web.Models.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloggingApp.Web.DataBase.EntityConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Users)
            .WithOne(e => e.Country)
            .HasForeignKey(e => e.CountryId);
    }
}