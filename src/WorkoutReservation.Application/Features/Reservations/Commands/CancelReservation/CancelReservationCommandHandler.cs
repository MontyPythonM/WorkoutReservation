using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;

public record CancelReservationCommand(int ReservationId) : IRequest;

internal sealed class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand>
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CancelReservationCommandHandler(ICurrentUserAccessor currentUserAccessor,
        IRealWorkoutRepository realWorkoutRepository, IReservationRepository reservationRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _currentUserAccessor = currentUserAccessor;
        _realWorkoutRepository = realWorkoutRepository;
        _reservationRepository = reservationRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(CancelReservationCommand request, CancellationToken token)
    {
        var user = await _currentUserAccessor.GetUserAsync(token);
        var realWorkout = await _realWorkoutRepository
            .GetByReservationIdAsync(request.ReservationId, false, token);

        if (realWorkout is null)
            throw new NotFoundException(nameof(RealWorkout), request.ReservationId.ToString());

        var reservation = await _reservationRepository
            .GetByIdAsync(request.ReservationId, false, token);
        
        realWorkout.CancelReservation(reservation, _dateTimeProvider.GetNow());
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        return Unit.Value;
    }
}
