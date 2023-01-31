using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTags;

public record GetWorkoutTypeTagsQuery : IRequest<List<WorkoutTypeTagsDto>>;

internal sealed class GetWorkoutTypeTagsListQueryHandler : IRequestHandler<GetWorkoutTypeTagsQuery, List<WorkoutTypeTagsDto>>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly IMapper _mapper;

    public GetWorkoutTypeTagsListQueryHandler(IWorkoutTypeTagRepository workoutTypeTagRepository, IMapper mapper)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _mapper = mapper;
    }

    public async Task<List<WorkoutTypeTagsDto>> Handle(GetWorkoutTypeTagsQuery request, CancellationToken token)
    {
        var workoutTypeTags = await _workoutTypeTagRepository.GetAllAsync(true, token, incl => incl.WorkoutTypes);
        
        if (!workoutTypeTags.Any())
            throw new NotFoundException("Workout type tags not found.");

        return _mapper.Map<List<WorkoutTypeTagsDto>>(workoutTypeTags);
    }
}