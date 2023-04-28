using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;
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
            .GetByIdAsync(request.Id, false, token, 
                incl => incl.WorkoutTypeTags, incl => incl.Instructors);
        
        if (workoutType is null)
            throw new NotFoundException(nameof(WorkoutType), request.Id.ToString());
        
        var validator = new UpdateWorkoutTypeCommandValidatior();
        await validator.ValidateAndThrowAsync(request, token);
        
        var tags = await _workoutTypeTagRepository
            .GetAllAsync(tag => request.WorkoutTypeTags.Contains(tag.Id), false, token);
        
        var instructors = await _instructorRepository
            .GetAllAsync(instructor => request.Instructors.Contains(instructor.Id), false, token);

        workoutType.Update(request.Name, request.Description, request.Intensity, instructors, tags);
        
        await _workoutTypeRepository.UpdateAsync(workoutType, token);
        return Unit.Value;
    }
}
