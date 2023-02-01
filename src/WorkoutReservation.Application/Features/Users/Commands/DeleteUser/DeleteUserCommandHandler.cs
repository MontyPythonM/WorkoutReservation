using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserGuid) : IRequest;

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
        var userToRemove = await _userRepository.GetByGuidAsync(request.UserGuid, false, token);

        if (userToRemove is null)
            throw new NotFoundException($"User with Guid: {request.UserGuid} not found.");

        var currentUserGuid = _currentUserAccessor.GetCurrentUserId();

        var validator = new DeleteUserCommandValidator(currentUserGuid);
        await validator.ValidateAndThrowAsync(request, token);

        await _userRepository.DeleteAsync(userToRemove, token);
        
        _logger.LogInformation($"User with Id: {request.UserGuid} was deleted by Administrator Id: {currentUserGuid}.");
        return Unit.Value;
    }
}
