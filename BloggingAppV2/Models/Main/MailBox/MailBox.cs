using BloggingApp.Web.Models.Identity;

namespace BloggingApp.Web.Models.Main;

public class MailBox
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public List<MailBoxMessage> Messages { get; set; } = new();
}