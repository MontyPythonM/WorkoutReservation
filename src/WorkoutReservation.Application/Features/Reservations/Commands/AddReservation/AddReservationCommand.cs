using MediatR;

namespace WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;

public class AddReservationCommand : IRequest<int>
{
    public int RealWorkoutId { get; set; }
}
