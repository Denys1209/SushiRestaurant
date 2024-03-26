using MailKit.Net.Smtp;
using MimeKit;


namespace SushiRestaurant.WebApi.EmailService;
public class EmailSender : IEmailSender
{

    private readonly IConfiguration _configuration;
    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(EmailSendRequest emailSendRequest)
    {
        // Create a new MimeMessage
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sushi Restaurant", _configuration["EmailSender:From"]));
        message.To.Add(new MailboxAddress("", emailSendRequest.To));
        message.Subject = emailSendRequest.Subject;

        // Set the email body
        message.Body = new TextPart("html")
        {
            Text = $"<h1>User Registered</h1><br /><h2>{emailSendRequest.Content}</h2>"
        };

        // Use MailKit's SmtpClient to send the email
        using (var client = new SmtpClient())
        {
            client.CheckCertificateRevocation = false;
            client.Connect(_configuration["EmailSender:Connect"], 587, false);
            client.Authenticate(_configuration["EmailSender:AuthenticateName"], _configuration["EmailSender:AuthenticatePassword"]);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
