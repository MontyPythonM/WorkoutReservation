using FluentValidation;

namespace WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;

internal sealed class EditReservationCommandValidator : AbstractValidator<EditReservationCommand>
{
    public EditReservationCommandValidator()
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.ReservationStatus)
            .IsInEnum()
            .NotEmpty();

        RuleFor(x => x.Note)
            .MaximumLength(3000);
    }
}
