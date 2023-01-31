using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IApplicationRoleRepository _roleRepository;

    public SetUserRoleCommandHandler(IApplicationUserRepository userRepository, 
        ICurrentUserAccessor currentUserAccessor, IApplicationRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _currentUserAccessor = currentUserAccessor;
        _roleRepository = roleRepository;
    }

    public async Task<Unit> Handle(SetUserRoleCommand request, CancellationToken token)
    {
        var user = await _userRepository
            .GetByGuidAsync(request.UserId, false, token, incl => incl.ApplicationRoles);
        
        var validator = new SetUserRoleCommandValidation(_currentUserAccessor.GetCurrentUserId());
        await validator.ValidateAndThrowAsync(request, token);

        var role = await _roleRepository.GetAsync(request.Role, token);
        user.SetRole(role);
        
        await _userRepository.UpdateAsync(user, token);
        return Unit.Value;
    }
}
