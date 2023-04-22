using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;

public record DeleteRealWorkoutCommand(int Id) : IRequest;

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
        var realWorkout = await _realWorkoutRepository.GetByIdAsync(request.Id, false, token);
        
        if (realWorkout is null)
            throw new NotFoundException(nameof(RealWorkout), request.Id.ToString());

        await _realWorkoutRepository.DeleteAsync(realWorkout, token);
        
        _logger.LogInformation($"Real workout with Id: {request.Id} was deleted.");
        return Unit.Value;
    }
}
