using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTagsList;

public class GetWorkoutTypeTagsListQueryHandler : IRequestHandler<GetWorkoutTypeTagsListQuery, List<WorkoutTypeTagsListDto>>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly IMapper _mapper;

    public GetWorkoutTypeTagsListQueryHandler(IWorkoutTypeTagRepository workoutTypeTagRepository, IMapper mapper)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _mapper = mapper;
    }

    public async Task<List<WorkoutTypeTagsListDto>> Handle(GetWorkoutTypeTagsListQuery request, CancellationToken token)
    {
        List<WorkoutTypeTag> workoutTypeTags;
        if (request.OnlyActive)
        {
            workoutTypeTags = await _workoutTypeTagRepository.GetAllAsync(tag => tag.IsActive, true, token);
        }
        else
        {
            workoutTypeTags = await _workoutTypeTagRepository.GetAllAsync(true, token);
        }
        
        if (!workoutTypeTags.Any())
            throw new NotFoundException("Workout type tags not found.");

        return _mapper.Map<List<WorkoutTypeTagsListDto>>(workoutTypeTags);
    }
}