using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;

public class UpdateWorkoutTypeCommandHandler : IRequestHandler<UpdateWorkoutTypeCommand>
{
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;

    public UpdateWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository, 
        IWorkoutTypeTagRepository workoutTypeTagRepository)
    {
        _workoutTypeRepository = workoutTypeRepository;        
        _workoutTypeTagRepository = workoutTypeTagRepository;
    }

    public async Task<Unit> Handle(UpdateWorkoutTypeCommand request, CancellationToken token)
    {
        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.Id, false, token, x => x.WorkoutTypeTags);
        if (workoutType is null)
            throw new NotFoundException($"Workout type with Id: {request.Id} not found.");

        var validator = new UpdateWorkoutTypeCommandValidatior();
        await validator.ValidateAndThrowAsync(request, token);
        
        var tags = await _workoutTypeTagRepository
            .GetAllAsync(tag => request.WorkoutTypeTags.Contains(tag.Id), false, token);
        
        workoutType.Name = request.Name;
        workoutType.Description = request.Description;
        workoutType.Intensity = request.Intensity;
        
        workoutType.WorkoutTypeTags.Clear();
        
        foreach (var tag in tags)
        {
            workoutType.WorkoutTypeTags.Add(tag);
        }
        
        await _workoutTypeRepository.UpdateAsync(workoutType, token);
        return Unit.Value;
    }
}
