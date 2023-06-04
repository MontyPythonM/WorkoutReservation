using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly SendgridEmailSettings _sendgridEmailSettings;
    private readonly ILogger<EmailSender> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public EmailSender(SendgridEmailSettings sendgridEmailSettings, ILogger<EmailSender> logger,
        IDateTimeProvider dateTimeProvider)
    {
        _sendgridEmailSettings = sendgridEmailSettings;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task SendEmail(SendGridMessage message, CancellationToken token)
    {
        if (_sendgridEmailSettings.EnableDelivery)
        {
            var client = new SendGridClient(_sendgridEmailSettings.ApiKey);
            
            if (!string.IsNullOrWhiteSpace(_sendgridEmailSettings.DeliveryAddress))
            {
                message.Personalizations = new List<Personalization>();
                message.AddTo(new EmailAddress(_sendgridEmailSettings.DeliveryAddress));
            }

            var response = await client.SendEmailAsync(message, token);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Email sent to: '{string.Join(", ", message.ReplyTos.Select(to => to.Email))}' " +
                                 $"with subject: '{message.Subject}' failed at {_dateTimeProvider.GetNow()}. " +
                                 $"Response status code: {response.StatusCode}. Response body: {response.Body.ToString()}");
            }
        }
    }
}