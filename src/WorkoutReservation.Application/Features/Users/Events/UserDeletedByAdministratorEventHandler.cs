using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Events;

namespace WorkoutReservation.Application.Features.Users.Events;

internal sealed class UserDeletedByAdministratorEventHandler : INotificationHandler<UserDeletedByAdministratorEvent>
{
    private const string Subject = "Application user was deleted";
    private readonly IEmailSender _emailSender;
    private readonly IEmailBuilder _emailBuilder;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public UserDeletedByAdministratorEventHandler(IEmailSender emailSender, IEmailBuilder emailBuilder, 
        IApplicationUserRepository applicationUserRepository)
    {
        _emailSender = emailSender;
        _emailBuilder = emailBuilder;
        _applicationUserRepository = applicationUserRepository;
    }
    
    public async Task Handle(UserDeletedByAdministratorEvent @event, CancellationToken token)
    {
        var deletedUser = await _applicationUserRepository.GetByGuidAsync(@event.UserId, true, token);

        if (deletedUser is null || !deletedUser.IsDeleted)
        {
            return;
        }

        var removedUserContent = $"Your account was deleted by application administrator at " +
                                 $"{@event.DeletedAt.ToString()} and all your sensitive personal data has been anonymized.";
        
        var administratorsContent = $"Application user with ID: {@event.UserId} was successfully deleted at " +
                                    $"{@event.DeletedAt.ToString()}. His sensitive personal data has been anonymized.";
        
        var systemAdmins = await _applicationUserRepository
            .GetByRoleAsync(ApplicationRole.SystemAdministrator, true, token);
        
        var messageToUser = _emailBuilder
            .CreateSendGridMessage(@event.Email, removedUserContent, Subject);
        
        var messageToAdministrators = _emailBuilder
            .CreateSendGridMessage(systemAdmins.Select(admin => admin.Email), administratorsContent, Subject);
        
        await _emailSender.SendEmail(messageToUser, token);
        await _emailSender.SendEmail(messageToAdministrators, token);
    }
}