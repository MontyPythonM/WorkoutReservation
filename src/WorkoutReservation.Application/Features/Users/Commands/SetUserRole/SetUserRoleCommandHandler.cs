using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public SetUserRoleCommandHandler(IUserRepository userRepository, 
                                     ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(SetUserRoleCommand request, 
                                   CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByGuidAsync(request.UserGuid, cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with Guid: {request.UserGuid} not found.");

        var currentUserGuid = Guid.Parse(_currentUserService.UserId);

        var validator = new SetUserRoleCommandValidation(currentUserGuid);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        user.UserRole = request.UserRole;

        await _userRepository.UpdateAsync(user, cancellationToken);

        return Unit.Value;
    }
}
