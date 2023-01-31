using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;

public record UpdateWorkoutTypeCommand(int Id, string Name, string Description, WorkoutIntensity Intensity, 
    List<int> WorkoutTypeTags, List<int> Instructors) : IRequest;

internal sealed class UpdateWorkoutTypeCommandHandler : IRequestHandler<UpdateWorkoutTypeCommand>
{
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly IInstructorRepository _instructorRepository;

    public UpdateWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository, 
        IWorkoutTypeTagRepository workoutTypeTagRepository,
        IInstructorRepository instructorRepository)
    {
        _workoutTypeRepository = workoutTypeRepository;        
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _instructorRepository = instructorRepository;
    }

    public async Task<Unit> Handle(UpdateWorkoutTypeCommand request, CancellationToken token)
    {
        var workoutType = await _workoutTypeRepository
            .GetByIdAsync(request.Id, false, token, incl => incl.WorkoutTypeTags, incl => incl.Instructors);
        if (workoutType is null)
            throw new NotFoundException($"Workout type with Id: {request.Id} not found.");

        var validator = new UpdateWorkoutTypeCommandValidatior();
        await validator.ValidateAndThrowAsync(request, token);
        
        var tags = await _workoutTypeTagRepository
            .GetAllAsync(tag => request.WorkoutTypeTags.Contains(tag.Id), false, token);
        
        var instructors = await _instructorRepository
            .GetAllAsync(instructor => request.Instructors.Contains(instructor.Id), false, token);

        workoutType.Name = request.Name;
        workoutType.Description = request.Description;
        workoutType.Intensity = request.Intensity;
        
        workoutType.WorkoutTypeTags.Clear();
        tags.ToList().ForEach(tag => workoutType.WorkoutTypeTags.Add(tag));
        
        workoutType.Instructors.Clear();
        instructors.ToList().ForEach(instructor => workoutType.Instructors.Add(instructor));
        
        await _workoutTypeRepository.UpdateAsync(workoutType, token);
        return Unit.Value;
    }
}
