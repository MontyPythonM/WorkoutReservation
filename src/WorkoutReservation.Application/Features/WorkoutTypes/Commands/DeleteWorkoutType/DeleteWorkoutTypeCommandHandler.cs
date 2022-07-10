using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;

public class DeleteWorkoutTypeCommandHandler : IRequestHandler<DeleteWorkoutTypeCommand>
{
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    //private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    //private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ILogger<DeleteWorkoutTypeCommandHandler> _logger;

    public DeleteWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository,
                                           //IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
                                           //IRealWorkoutRepository realWorkoutRepository,
                                           ILogger<DeleteWorkoutTypeCommandHandler> logger)
    {
        _workoutTypeRepository = workoutTypeRepository;
        //_repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        //_realWorkoutRepository = realWorkoutRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkoutTypeCommand request, 
                                   CancellationToken cancellationToken)
    {
        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId);

        if (workoutType is null)
            throw new NotFoundException($"Workout type with Id: {request.WorkoutTypeId} not found.");

        //var repetitiveWorkouts = await _repetitiveWorkoutRepository.GetAllAsync();
        //var realWorkouts = await _realWorkoutRepository.GetAllAsync();

        //if (repetitiveWorkouts.Any() || realWorkouts.Any())            
        //    throw new ForbidException($"WorkoutType with Id: {request.WorkoutTypeId} is assigned to workouts (repetitiveWorkout or realWorkout). To delete an WorkoutType, you must first delete or edit the assigned workouts.");
        
        await _workoutTypeRepository.DeleteAsync(workoutType);

        _logger.LogInformation($"WorkoutType with Id: {request.WorkoutTypeId} was deleted.");

        return Unit.Value;
    }
}
