using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;

public record DeleteWorkoutTypeCommand(int WorkoutTypeId) : IRequest;

internal sealed class DeleteWorkoutTypeCommandHandler : IRequestHandler<DeleteWorkoutTypeCommand>
{
    private readonly IWorkoutTypeRepository _workoutTypeRepository;    
    private readonly ILogger<DeleteWorkoutTypeCommandHandler> _logger;

    public DeleteWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository,
        ILogger<DeleteWorkoutTypeCommandHandler> logger)
    {
        _workoutTypeRepository = workoutTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkoutTypeCommand request, CancellationToken token)
    {
        var workoutType = await _workoutTypeRepository
            .GetByIdAsync(request.WorkoutTypeId, false, token, 
                incl => incl.RealWorkouts, incl => incl.RepetitiveWorkouts);

        if (workoutType is null)
            throw new NotFoundException(nameof(WorkoutType), request.WorkoutTypeId.ToString());
        
        if (workoutType.RealWorkouts.Any() || workoutType.RepetitiveWorkouts.Any())
            throw new HasAssignedWorkoutsException(request.WorkoutTypeId);
        
        await _workoutTypeRepository.DeleteAsync(workoutType, token);

        _logger.LogInformation($"WorkoutType with Id: {request.WorkoutTypeId} was deleted.");
        return Unit.Value;
    }
}
