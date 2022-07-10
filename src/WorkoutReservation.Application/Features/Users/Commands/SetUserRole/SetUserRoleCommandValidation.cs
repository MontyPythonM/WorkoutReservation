using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public class SetUserRoleCommandValidation : AbstractValidator<SetUserRoleCommand>
{
    public SetUserRoleCommandValidation(Guid currentUser)
    {
        RuleFor(x => x.UserRole)
            .IsInEnum()
            .NotEmpty();

        RuleFor(x => x.UserGuid)
            .NotEmpty()
            .Must(x => x != currentUser)
            .WithMessage("You cannot change your own UserRole.");
    }
}
