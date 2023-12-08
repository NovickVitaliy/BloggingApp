using System.ComponentModel.DataAnnotations.Schema;
using BloggingApp.Web.Models.Main;


[Table("MailBoxMessages")]
public abstract class MailBoxMessage
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? SentAt { get; set; }
    public bool IsRead { get; set; }
    public Guid MailBoxId { get; set; }
    public MailBox MailBox { get; set; }
}