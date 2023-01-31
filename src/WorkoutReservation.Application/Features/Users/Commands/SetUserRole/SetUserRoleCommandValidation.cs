using FluentValidation;

namespace WorkoutReservation.Application.Features.Users.Commands.SetUserRole;

public class SetUserRoleCommandValidation : AbstractValidator<SetUserRoleCommand>
{
    public SetUserRoleCommandValidation(Guid currentUser)
    {
        RuleFor(x => x.Role)
            .IsInEnum()
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty()
            .Must(x => x != currentUser)
            .WithMessage("You cannot change your own UserRole.");
    }
}
