using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;

public record EditReservationCommand(int ReservationId, ReservationStatus ReservationStatus, string Note) : IRequest;

internal sealed class EditReservationCommandHandler : IRequestHandler<EditReservationCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;

    public EditReservationCommandHandler(IRealWorkoutRepository realWorkoutRepository)
    {
        _realWorkoutRepository = realWorkoutRepository;
    }

    public async Task<Unit> Handle(EditReservationCommand request, CancellationToken token)
    {
        var realWorkout = await _realWorkoutRepository
            .GetByReservationIdAsync(request.ReservationId, false, token);

        if (realWorkout is null)
            throw new NotFoundException($"RealWorkout containing a reservation with Id: {request.ReservationId} not found");
        
        var reservation = realWorkout.Reservations.First(r => r.Id == request.ReservationId);

        realWorkout.UpdateReservation(reservation, request.ReservationStatus, request.Note);
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        
        return Unit.Value;
    }
}
