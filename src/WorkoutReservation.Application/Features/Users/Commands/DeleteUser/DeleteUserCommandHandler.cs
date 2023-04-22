using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(IApplicationUserRepository userRepository,
        ICurrentUserAccessor currentUserAccessor,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _currentUserAccessor = currentUserAccessor;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken token)
    {
        var currentUser = await _currentUserAccessor.GetUserAsync(token);
        var userToRemove = await _userRepository.GetByGuidAsync(request.Id, false, token);

        if (userToRemove is null)
            throw new NotFoundException(nameof(ApplicationUser), request.Id.ToString());
        
        if (userToRemove.Id == currentUser.Id)
            throw new UserCannotDeleteOwnAccount();

        userToRemove.SoftDeleteUser();
        await _userRepository.UpdateAsync(userToRemove, token);
        
        _logger.LogInformation($"User with Id: {request.Id} was soft deleted by user Id: {currentUser.Id}.");
        return Unit.Value;
    }
}
