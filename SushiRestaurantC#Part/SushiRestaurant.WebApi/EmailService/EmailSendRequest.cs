namespace SushiRestaurant.WebApi.EmailService;
public class EmailSendRequest
{

    public required string To { get; set; }
    public required string Subject { get; set; }
    public required string Content { get; set; }
}
