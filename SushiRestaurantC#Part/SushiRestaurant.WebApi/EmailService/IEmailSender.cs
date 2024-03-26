namespace SushiRestaurant.WebApi.EmailService;
public interface IEmailSender
{
    void SendEmail(EmailSendRequest emailSendRequest);
}
