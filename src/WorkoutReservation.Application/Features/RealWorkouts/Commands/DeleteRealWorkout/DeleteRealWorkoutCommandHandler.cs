using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;

public class DeleteRealWorkoutCommandHandler : IRequestHandler<DeleteRealWorkoutCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ILogger<DeleteRealWorkoutCommandHandler> _logger;

    public DeleteRealWorkoutCommandHandler(IRealWorkoutRepository realWorkoutRepository, 
                                           ILogger<DeleteRealWorkoutCommandHandler> logger)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteRealWorkoutCommand request, 
                                   CancellationToken cancellationToken)
    {
        var realWorkout = await _realWorkoutRepository.GetByIdAsync(request.RealWorkoutId, cancellationToken);

        if (realWorkout is null)
            throw new NotFoundException($"Real workout with Id: {request.RealWorkoutId} not found.");

        await _realWorkoutRepository.DeleteAsync(realWorkout, cancellationToken);

        _logger.LogInformation($"Real workout with Id: {request.RealWorkoutId} was deleted.");

        return Unit.Value;
    }
}
