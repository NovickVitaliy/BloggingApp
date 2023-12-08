using BloggingApp.Web.Models.Main;
using Microsoft.AspNetCore.Identity;

namespace BloggingAppV2.Models.Main.Identity;

public class User : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public string? Description { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public DateOnly? CreatedAt { get; set; }
    public Photo Photo { get; set; }
    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
    
    //public Guid? MailBoxId { get; set; }
    //public MailBox? MailBox { get; set; }
    //public List<UserFriendship>? Friends { get; set; }
}