using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;

public class CreateWorkoutTypeTagCommandHandler : IRequestHandler<CreateWorkoutTypeTagCommand, int>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly IMapper _mapper;

    public CreateWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository, IMapper mapper)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateWorkoutTypeTagCommand request, CancellationToken token)
    {
        var workoutTypeTag = _mapper.Map<WorkoutTypeTag>(request);
        workoutTypeTag.IsActive = true;
        workoutTypeTag = await _workoutTypeTagRepository.AddAsync(workoutTypeTag, token);
        
        return workoutTypeTag.Id;
    }
}