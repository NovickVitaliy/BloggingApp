using System.ComponentModel.DataAnnotations.Schema;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Models.Main;

[Table("Photos")]
public class Photo
{
    public Guid Id { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}