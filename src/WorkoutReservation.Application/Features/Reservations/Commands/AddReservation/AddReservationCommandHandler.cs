using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;

public record AddReservationCommand(int RealWorkoutId) : IRequest;

internal sealed class AddReservationCommandHandler : IRequestHandler<AddReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public AddReservationCommandHandler(IReservationRepository reservationRepository, 
        IRealWorkoutRepository realWorkoutRepository,
        ICurrentUserAccessor currentUserAccessor)
    {
        _reservationRepository = reservationRepository;
        _realWorkoutRepository = realWorkoutRepository;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<Unit> Handle(AddReservationCommand request, CancellationToken token)
    {
        var user = await _currentUserAccessor.GetUserAsync(token);

        var realWorkout = await _realWorkoutRepository
            .GetByIdAsync(request.RealWorkoutId, false, token);
        if (realWorkout is null)
            throw new NotFoundException($"Real workout with Id: {request.RealWorkoutId} not found.");
        
        var isUserAlreadyReservedWorkout = await _reservationRepository
            .CheckIsReservedAsync(realWorkout, user, token);
        var validator = new AddReservationCommandValidator(realWorkout, user.Id, isUserAlreadyReservedWorkout);
        await validator.ValidateAndThrowAsync(request, token);

        realWorkout.IncrementCurrentParticipantNumber();
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        
        var reservation = new Reservation(realWorkout, user);
        await _reservationRepository.AddReservationAsync(reservation, token);
        return Unit.Value;
    }
}
