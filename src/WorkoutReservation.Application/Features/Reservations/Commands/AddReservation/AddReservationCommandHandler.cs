using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;

public record AddReservationCommand(int RealWorkoutId) : IRequest;

internal sealed class AddReservationCommandHandler : IRequestHandler<AddReservationCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AddReservationCommandHandler(IRealWorkoutRepository realWorkoutRepository,
        ICurrentUserAccessor currentUserAccessor, IDateTimeProvider dateTimeProvider)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _currentUserAccessor = currentUserAccessor;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(AddReservationCommand request, CancellationToken token)
    {
        var user = await _currentUserAccessor.GetUserAsync(token);

        var realWorkout = await _realWorkoutRepository
            .GetByIdWithReservationUserAsync(request.RealWorkoutId, false, token);
        
        if (realWorkout is null)
            throw new NotFoundException(nameof(RealWorkout), request.RealWorkoutId.ToString());
        
        realWorkout.AddReservation(user, _dateTimeProvider.GetNow());
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        
        return Unit.Value;
    }
}
