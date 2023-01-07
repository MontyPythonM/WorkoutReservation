using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;

public class EditReservationStatusCommandHandler : IRequestHandler<EditReservationStatusCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;

    public EditReservationStatusCommandHandler(IReservationRepository reservationRepository,
        IRealWorkoutRepository realWorkoutRepository)
    {
        _reservationRepository = reservationRepository;
        _realWorkoutRepository = realWorkoutRepository;
    }

    public async Task<Unit> Handle(EditReservationStatusCommand request, CancellationToken token)
    {
        var reservation = await _reservationRepository
            .GetByIdAsync(request.ReservationId, false, token,
                incl => incl.RealWorkout);
        
        if (reservation is null)
            throw new NotFoundException($"Reservation with Id: {request.ReservationId} not found.");

        var validator = new EditReservationStatusCommandValidator();
        await validator.ValidateAndThrowAsync(request, token);
        
        if (reservation.ReservationStatus == ReservationStatus.Reserved && 
            request.ReservationStatus != ReservationStatus.Reserved)
        {
            reservation.RealWorkout.DecrementCurrentParticipantNumber();
        }

        if (reservation.ReservationStatus != ReservationStatus.Reserved && 
            request.ReservationStatus == ReservationStatus.Reserved)
        {
            reservation.RealWorkout.IncrementCurrentParticipantNumber();
        }

        reservation.UpdateLastModificationDate();
        reservation.SetReservationStatus(request.ReservationStatus);
        
        await _reservationRepository.UpdateAsync(reservation, token);
        return Unit.Value;
    }
}
