﻿using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;

public class DeleteWorkoutTypeCommandHandler : IRequestHandler<DeleteWorkoutTypeCommand>
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
        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId, token);

        if (workoutType is null)
            throw new NotFoundException($"Workout type with Id: {request.WorkoutTypeId} not found.");
        
        if (workoutType.BaseWorkouts.Any())
            throw new BadRequestException($"WorkoutType with Id: {request.WorkoutTypeId} is assigned to existing workouts (repetitiveWorkout or realWorkout). " +
                                          $"To delete an WorkoutType, you must first delete or edit the assigned workouts.");
        
        await _workoutTypeRepository.DeleteAsync(workoutType, token);

        _logger.LogInformation($"WorkoutType with Id: {request.WorkoutTypeId} was deleted.");
        return Unit.Value;
    }
}
