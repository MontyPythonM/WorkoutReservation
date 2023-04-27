using SendGrid.Helpers.Mail;

namespace WorkoutReservation.Application.Contracts;

public interface IEmailBuilder
{
    SendGridMessage CreateSendGridMessage(string emailAddress, string content, string subject);
    SendGridMessage CreateSendGridMessage(IEnumerable<string> emailAddresses, string content, string subject);
}