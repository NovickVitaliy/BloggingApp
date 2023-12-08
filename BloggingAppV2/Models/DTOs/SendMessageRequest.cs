using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.Main;
using BloggingAppV2.Models.Main.Identity;

namespace BloggingApp.Web.Models.DTOs;

public class SendMessageRequest
{
    public User? Sender { get; set; }
    public User? Receiver { get; set; }
    public MessageType MessageType { get; set; }
}