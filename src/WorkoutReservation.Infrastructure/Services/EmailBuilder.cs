﻿using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Infrastructure.Exceptions;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Services;

public class EmailBuilder : IEmailBuilder
{
    private readonly SendgridEmailSettings _settings;
    private readonly ILogger<EmailBuilder> _logger;

    public EmailBuilder(IOptionsSnapshot<SendgridEmailSettings> snapshot, ILogger<EmailBuilder> logger)
    {
        _settings = snapshot.Value;
        _logger = logger;
    }
    
    public SendGridMessage CreateSendGridMessage(IEnumerable<string> emailAddresses, string content, string subject)
    {
        var from = new EmailAddress(_settings.FromAddress, _settings.FromName);
        var tos = new List<EmailAddress>();
        
        foreach (var email in emailAddresses)
        {
            if (!IsEmailAddressFormatValid(email) || string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailAddressFormatException(email);
            }

            tos.Add(new EmailAddress(email));
        }
        
        return MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, content, content);
    }

    public SendGridMessage CreateSendGridMessage(string emailAddress, string content, string subject) => 
        CreateSendGridMessage(new List<string> { emailAddress }, content, subject);
    
    private bool IsEmailAddressFormatValid(string email) => new EmailAddressAttribute().IsValid(email);
}