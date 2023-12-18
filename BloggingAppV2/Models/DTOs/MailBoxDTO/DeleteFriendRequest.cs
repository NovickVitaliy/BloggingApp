namespace BloggingApp.Web.Models.DTOs;

public class DeleteFriendRequest
{
    public Guid DeleterId { get; set; }
    public Guid FriendToDeleteId { get; set; }
}