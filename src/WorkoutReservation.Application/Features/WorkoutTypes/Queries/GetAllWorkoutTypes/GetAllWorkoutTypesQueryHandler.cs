using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetAllWorkoutTypes;

public record GetAllWorkoutTypesQuery() : IRequest<List<WorkoutTypesDto>>;

internal sealed class GetAllWorkoutTypesQueryHandler : IRequestHandler<GetAllWorkoutTypesQuery, List<WorkoutTypesDto>>
{
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IMapper _mapper;

    public GetAllWorkoutTypesQueryHandler(IWorkoutTypeRepository workoutTypeRepository, IMapper mapper)
    {
        _workoutTypeRepository = workoutTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<WorkoutTypesDto>> Handle(GetAllWorkoutTypesQuery request, CancellationToken token)
    {
        var workoutTypes = await _workoutTypeRepository.GetAllAsync(true, token);
        return _mapper.Map<List<WorkoutTypesDto>>(workoutTypes);
    }
}