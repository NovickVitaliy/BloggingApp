using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Models.Main;

public class Country
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? FlagUrl { get; set; }
    public List<User>? Users { get; set; }
}