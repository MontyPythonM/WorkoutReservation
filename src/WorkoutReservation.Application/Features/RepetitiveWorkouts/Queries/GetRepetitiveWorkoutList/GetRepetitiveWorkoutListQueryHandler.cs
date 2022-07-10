using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;

public class GetRepetitiveWorkoutListQueryHandler : IRequestHandler<GetRepetitiveWorkoutListQuery, 
                                                                    List<RepetitiveWorkoutListDto>>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly IMapper _mapper;

    public GetRepetitiveWorkoutListQueryHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository, 
                                                IMapper mapper)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _mapper = mapper;
    }

    public async Task<List<RepetitiveWorkoutListDto>> Handle(GetRepetitiveWorkoutListQuery request, 
                                                             CancellationToken cancellationToken)
    {
        var repetitiveWorkouts = await _repetitiveWorkoutRepository.GetAllAsync();

        if (!repetitiveWorkouts.Any())
            throw new NotFoundException("Repetitive workouts not found.");

        return _mapper.Map<List<RepetitiveWorkoutListDto>>(repetitiveWorkouts);
    }
}
