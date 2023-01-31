using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetActiveWorkoutTypeTags;

public record GetActiveWorkoutTypeTagsQuery : IRequest<List<ActiveWorkoutTypeTagsDto>>;
    
internal sealed class GetActiveWorkoutTypeTagsQueryHandler : IRequestHandler<GetActiveWorkoutTypeTagsQuery, List<ActiveWorkoutTypeTagsDto>>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly IMapper _mapper;

    public GetActiveWorkoutTypeTagsQueryHandler(IWorkoutTypeTagRepository workoutTypeTagRepository, IMapper mapper)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _mapper = mapper;
    }

    public async Task<List<ActiveWorkoutTypeTagsDto>> Handle(GetActiveWorkoutTypeTagsQuery request, CancellationToken token)
    {
        var activeWorkoutTypeTags = await _workoutTypeTagRepository
            .GetAllAsync(tag => tag.IsActive || tag.WorkoutTypes.Count > 0, true, token);
        
        if (!activeWorkoutTypeTags.Any())
            throw new NotFoundException("Workout type tags not found.");

        return _mapper.Map<List<ActiveWorkoutTypeTagsDto>>(activeWorkoutTypeTags);
    }
}