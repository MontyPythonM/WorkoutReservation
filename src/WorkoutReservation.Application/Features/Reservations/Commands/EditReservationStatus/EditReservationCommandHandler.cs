using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;

public record EditReservationCommand(int ReservationId, ReservationStatus ReservationStatus, string Note) : IRequest;

internal sealed class EditReservationCommandHandler : IRequestHandler<EditReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;

    public EditReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Unit> Handle(EditReservationCommand request, CancellationToken token)
    {
        var reservation = await _reservationRepository
            .GetByIdAsync(request.ReservationId, false, token,
                incl => incl.RealWorkout);
        
        if (reservation is null)
            throw new NotFoundException($"Reservation with Id: {request.ReservationId} not found.");

        var validator = new EditReservationCommandValidator();
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
        reservation.SetNote(request.Note);
        
        await _reservationRepository.UpdateAsync(reservation, token);
        return Unit.Value;
    }
}
