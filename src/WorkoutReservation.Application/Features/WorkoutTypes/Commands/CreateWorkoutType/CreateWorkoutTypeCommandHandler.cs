using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;

public record CreateWorkoutTypeCommand(string Name, string Description, WorkoutIntensity Intensity, 
    List<int> WorkoutTypeTags, List<int> Instructors) : IRequest;

internal sealed class CreateWorkoutTypeCommandHandler : IRequestHandler<CreateWorkoutTypeCommand>
{    
    private readonly IMapper _mapper;
    private readonly IWorkoutTypeRepository _workoutRepository;
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly IInstructorRepository _instructorRepository;

    public CreateWorkoutTypeCommandHandler(IMapper mapper,
        IWorkoutTypeRepository workoutTypeRepository,
        IWorkoutTypeTagRepository workoutTypeTagRepository,
        IInstructorRepository instructorRepository)
    {
        _mapper = mapper;
        _workoutRepository = workoutTypeRepository;        
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _instructorRepository = instructorRepository;        
    }

    public async Task<Unit> Handle(CreateWorkoutTypeCommand request, CancellationToken token)
    {
        var validator = new CreateWorkoutTypeCommandValidator();
        await validator.ValidateAndThrowAsync(request, token);
        
        var workoutType = _mapper.Map<WorkoutType>(request);
        
        var tags = await _workoutTypeTagRepository
            .GetAllAsync(tag => tag.IsActive && request.WorkoutTypeTags.Contains(tag.Id), false, token);
        
        var instructors = await _instructorRepository
            .GetAllAsync(instructor => request.Instructors.Contains(instructor.Id), false, token);
        
        workoutType.WorkoutTypeTags = tags;
        workoutType.Instructors = instructors;
        
        await _workoutRepository.AddAsync(workoutType, token);
        return Unit.Value;
    }
}
