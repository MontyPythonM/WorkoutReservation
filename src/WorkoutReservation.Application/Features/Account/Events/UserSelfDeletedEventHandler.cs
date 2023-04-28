using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Events;

namespace WorkoutReservation.Application.Features.Account.Events;

public class UserSelfDeletedEventHandler : INotificationHandler<UserSelfDeletedEvent>
{
    private const string Subject = "Application user was deleted";
    private readonly IEmailSender _emailSender;
    private readonly IEmailBuilder _emailBuilder;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public UserSelfDeletedEventHandler(IEmailSender emailSender, IEmailBuilder emailBuilder, 
        IApplicationUserRepository applicationUserRepository)
    {
        _emailSender = emailSender;
        _emailBuilder = emailBuilder;
        _applicationUserRepository = applicationUserRepository;
    }
    
    public async Task Handle(UserSelfDeletedEvent @event, CancellationToken token)
    {
        var deletedUser = await _applicationUserRepository.GetByGuidAsync(@event.UserId, true, token);

        if (deletedUser is null || !deletedUser.IsDeleted)
        {
            return;
        }

        var removedUserContent = $"Your account was deleted by application administrator at " +
                                 $"{@event.DeletedAt.ToString()} and all your sensitive personal data has been anonymized.";
        
        var message = _emailBuilder.CreateSendGridMessage(@event.Email, removedUserContent, Subject);
        
        await _emailSender.SendEmail(message, token);
    }
}