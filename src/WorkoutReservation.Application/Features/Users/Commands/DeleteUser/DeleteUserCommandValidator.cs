using FluentValidation;

namespace WorkoutReservation.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(Guid currentUserGuid)
    {
        RuleFor(x => x.UserGuid)
            .NotEmpty()
            .Must(userToRemoveGuid => userToRemoveGuid != currentUserGuid)
            .WithMessage("You cannot delete your own account by this endpoint. Use route: ../api/account/delete-account");
    }
}
