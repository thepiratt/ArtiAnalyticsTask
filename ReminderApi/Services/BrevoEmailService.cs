using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using ReminderApi.Interfaces;

namespace ReminderApi.Services;

public class BrevoEmailService(IConfiguration configuration, ILogger<BrevoEmailService> logger) : IEmailService
{
    private readonly string _apiKey = configuration["Brevo:ApiKey"] ?? throw new InvalidOperationException("Brevo:ApiKey is missing");
    private readonly string _senderEmail = configuration["Brevo:SenderEmail"] ?? throw new InvalidOperationException("Brevo:SenderEmail is missing");
    private readonly string _senderName = configuration["Brevo:SenderName"] ?? throw new InvalidOperationException("Brevo:SenderName is missing");

    public async System.Threading.Tasks.Task SendEmailAsync(string toEmail, string subject, string message)
    {
        try
        {
            brevo_csharp.Client.Configuration.Default.AddApiKey("api-key", _apiKey);

            var apiInstance = new TransactionalEmailsApi();

            var sendSmtpEmail = new SendSmtpEmail
            {
                To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(toEmail) },
                Sender = new SendSmtpEmailSender(_senderName, _senderEmail),
                Subject = subject,
                HtmlContent = $"<html><body>{message}</body></html>"
            };

            var response = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            logger.LogInformation("Email sent successfully to {Email}. Message ID: {MessageId}", toEmail, response.MessageId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {Email}", toEmail);
            throw;
        }
    }
}
