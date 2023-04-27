using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public DeleteUserCommandHandler(IApplicationUserRepository userRepository,
        ICurrentUserAccessor currentUserAccessor)
    {
        _userRepository = userRepository;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken token)
    {
        var userToRemove = await _userRepository.GetByGuidAsync(request.Id, false, token);

        if (userToRemove is null)
            throw new NotFoundException(nameof(ApplicationUser), request.Id.ToString());
        
        if (userToRemove.Id == _currentUserAccessor.GetUserId())
            throw new UserCannotDeleteOwnAccount();

        userToRemove.DeleteUser();
        
        await _userRepository.UpdateAsync(userToRemove, token);
        return Unit.Value;
    }
}
