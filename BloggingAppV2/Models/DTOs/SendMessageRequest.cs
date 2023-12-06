using BloggingApp.Web.Enums;
using BloggingApp.Web.Models.Identity;
using BloggingApp.Web.Models.Main;

namespace BloggingApp.Web.Models.DTOs;

public class SendMessageRequest
{
    public User? Sender { get; set; }
    public User? Receiver { get; set; }
    public MessageType MessageType { get; set; }
}