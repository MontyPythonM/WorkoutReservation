using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;

public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICurrentUserService _userService;
    private readonly IRealWorkoutRepository _realWorkoutRepository;

    public CancelReservationCommandHandler(IReservationRepository reservationRepository,
        ICurrentUserService userService,
        IRealWorkoutRepository realWorkoutRepository)
    {
        _reservationRepository = reservationRepository;
        _userService = userService;
        _realWorkoutRepository = realWorkoutRepository;
    }

    public async Task<Unit> Handle(CancelReservationCommand request, CancellationToken token)
    {
        var currentUserGuid = _userService.UserId.ToGuid();
        var reservation = await _reservationRepository
            .GetByIdAsync(request.ReservationId, false, token, 
                incl => incl.User, incl => incl.RealWorkout);
        
        if (reservation is null)
            throw new NotFoundException($"Reservation with Id: {request.ReservationId} not found.");
        
        var validator = new CancelReservationCommandValidator(reservation, currentUserGuid);
        await validator.ValidateAndThrowAsync(request, token);
        
        reservation.UpdateLastModificationDate();
        reservation.SetReservationStatus(ReservationStatus.Cancelled);
        
        var realWorkout = reservation.RealWorkout;
        realWorkout.DecrementCurrentParticipantNumber();
        
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        await _reservationRepository.UpdateAsync(reservation, token);
        return Unit.Value;
    }
}
