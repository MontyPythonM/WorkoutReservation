using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TinyHelpers.Extensions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Infrastructure.Exceptions;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly SendgridEmailSettings _settings;
    private readonly ILogger<EmailSender> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public EmailSender(IOptionsSnapshot<SendgridEmailSettings> snapshot, ILogger<EmailSender> logger,
        IDateTimeProvider dateTimeProvider)
    {
        _settings = snapshot.Value;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task SendEmail(SendGridMessage message, CancellationToken token)
    {
        if (_settings.EnableDelivery)
        {
            var client = new SendGridClient(_settings.ApiKey);
            
            if (!string.IsNullOrWhiteSpace(_settings.DeliveryAddress))
            {
                message.Personalizations = new List<Personalization>();
                message.AddTo(new EmailAddress(_settings.DeliveryAddress));
            }

            var response = await client.SendEmailAsync(message, token);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Email sent to: '{string.Join(", ", message.ReplyTos.Select(to => to.Email))}' " +
                                 $"with subject: '{message.Subject}' failed at {_dateTimeProvider.Now}. " +
                                 $"Response status code: {response.StatusCode}. Response body: {response.Body.ToString()}");
            }
        }
    }
}