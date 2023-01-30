using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    //private readonly IApplicationRolesRepository _rolesRepository;

    public SetUserRoleCommandHandler(IApplicationUserRepository userRepository, 
        ICurrentUserAccessor currentUserAccessor)
    {
        _userRepository = userRepository;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<Unit> Handle(SetUserRoleCommand request, CancellationToken token)
    {
        var user = await _userRepository.GetByGuidAsync(request.UserId, token);
        
        var validator = new SetUserRoleCommandValidation(_currentUserAccessor.GetCurrentUserId());
        await validator.ValidateAndThrowAsync(request, token);

        // TODO: role repository???
        //var role = _rolesRepository.GetAsync(request.RoleId, token);
        //user.SetRole(role);
        
        await _userRepository.UpdateAsync(user, token);
        return Unit.Value;
    }
}
