using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WebApiDemoMailkit;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> SendEmail(string toEmail, string mailSubject, string mailBody)
    {
        try
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var port = _configuration.GetValue<int?>("EmailSettings:Port");
            var fromMail = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];

            if (string.IsNullOrWhiteSpace(smtpServer) ||
                !port.HasValue ||
                string.IsNullOrWhiteSpace(fromMail) ||
                string.IsNullOrWhiteSpace(password))
            {
                return "Email sending failed. Error: Email settings are missing or invalid.";
            }

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Your Name", fromMail));
            email.To.Add(new MailboxAddress("To Name", toEmail));
            email.Subject = mailSubject;
            email.Body = new TextPart("html") { Text = mailBody };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpServer, port.Value, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(fromMail, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return "Email sent successfully!";
        }
        catch (Exception ex)
        {
            return $"Email sending failed. Error: {ex.Message}";
        }
    }
}