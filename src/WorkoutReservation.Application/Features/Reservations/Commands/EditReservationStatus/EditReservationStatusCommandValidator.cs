using FluentValidation;

namespace WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;

public class EditReservationStatusCommandValidator : AbstractValidator<EditReservationStatusCommand>
{
    public EditReservationStatusCommandValidator()
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.ReservationStatus)
            .IsInEnum()
            .NotEmpty();
    }
}
