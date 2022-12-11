using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;

public class CreateWorkoutTypeCommandHandler : IRequestHandler<CreateWorkoutTypeCommand, int>
{
    private readonly IWorkoutTypeRepository _workoutRepository;
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly IMapper _mapper;

    public CreateWorkoutTypeCommandHandler(IWorkoutTypeRepository workoutTypeRepository,
        IWorkoutTypeTagRepository workoutTypeTagRepository,
        IMapper mapper)
    {
        _workoutRepository = workoutTypeRepository;        
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateWorkoutTypeCommand request, CancellationToken token)
    {
        var validator = new CreateWorkoutTypeCommandValidator();
        await validator.ValidateAndThrowAsync(request, token);
        
        var workoutType = _mapper.Map<WorkoutType>(request);
        var tags = await _workoutTypeTagRepository
            .GetAllAsync(tag => tag.IsActive && request.WorkoutTypeTags.Contains(tag.Id), false, token);
        
        workoutType.WorkoutTypeTags = tags;
        workoutType = await _workoutRepository.AddAsync(workoutType, token);

        return workoutType.Id;
    }
}
