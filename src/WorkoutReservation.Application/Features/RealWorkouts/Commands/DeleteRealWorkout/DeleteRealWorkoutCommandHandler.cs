using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;

public record DeleteRealWorkoutCommand(int RealWorkoutId) : IRequest;

internal sealed class DeleteRealWorkoutCommandHandler : IRequestHandler<DeleteRealWorkoutCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ILogger<DeleteRealWorkoutCommandHandler> _logger;

    public DeleteRealWorkoutCommandHandler(IRealWorkoutRepository realWorkoutRepository, 
        ILogger<DeleteRealWorkoutCommandHandler> logger)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteRealWorkoutCommand request, CancellationToken token)
    {
        var realWorkout = await _realWorkoutRepository
            .GetByIdAsync(request.RealWorkoutId, false, token);
        
        if (realWorkout is null)
            throw new NotFoundException($"Real workout with Id: {request.RealWorkoutId} not found.");

        await _realWorkoutRepository.DeleteAsync(realWorkout, token);
        
        _logger.LogInformation($"Real workout with Id: {request.RealWorkoutId} was deleted.");
        return Unit.Value;
    }
}
