using SendGrid.Helpers.Mail;

namespace WorkoutReservation.Application.Contracts;

public interface IEmailSender
{
    Task SendEmail(SendGridMessage message, CancellationToken token);
}