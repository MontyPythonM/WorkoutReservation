using FluentValidation;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

internal sealed class SetUserRoleCommandValidation : AbstractValidator<SetUserRoleCommand>
{
    public SetUserRoleCommandValidation(Guid currentUser)
    {
        RuleFor(x => x.Roles)
            .ForEach(role => role.IsInEnum());

        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(currentUser);
    }
}
