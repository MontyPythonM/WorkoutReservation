using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public record SetUserRoleCommand(Guid Id, List<Role> Roles) : IRequest;

internal sealed class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand>
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
        var validator = new SetUserRoleCommandValidation(_currentUserAccessor.GetUserId());
        await validator.ValidateAndThrowAsync(request, token);
        
        var user = await _userRepository.GetByGuidAsync(request.Id, false, token);

        if (user is null)
            throw new NotFoundException(nameof(ApplicationUser), request.Id.ToString());
        
        var roles = await _roleRepository.GetAsync(request.Roles, token);
        user.SetRoles(roles);
        
        await _userRepository.UpdateAsync(user, token);
        return Unit.Value;
    }
}
