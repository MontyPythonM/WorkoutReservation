using AutoMapper;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Methods;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek
{
    public class GetRealWorkoutFromCurrentWeekQueryHandler : IRequestHandler<GetRealWorkoutFromCurrentWeekQuery, 
                                                                             List<RealWorkoutFromCurrentWeekDto>>
    {
        private readonly IRealWorkoutRepository _realWorkoutRepository;
        private readonly IMapper _mapper;

        public GetRealWorkoutFromCurrentWeekQueryHandler(IRealWorkoutRepository realWorkoutRepository,
                                                         IMapper mapper)
        {
            _realWorkoutRepository = realWorkoutRepository;
            _mapper = mapper;
        }

        public async Task<List<RealWorkoutFromCurrentWeekDto>> Handle(GetRealWorkoutFromCurrentWeekQuery request, 
                                                                      CancellationToken cancellationToken)
        {
            var firstDayOfCurrentWeek = DateTime.Now.GetFirstDayOfWeek();
            var lastDayOfCurrentWeek = firstDayOfCurrentWeek.AddDays(7);

            var realWorkouts = await _realWorkoutRepository.GetAllAsync(firstDayOfCurrentWeek, lastDayOfCurrentWeek);

            if (!realWorkouts.Any())
                throw new NotFoundException($"Real workouts from current week not found. [Date from: {firstDayOfCurrentWeek} to {lastDayOfCurrentWeek}");

            return _mapper.Map<List<RealWorkoutFromCurrentWeekDto>>(realWorkouts);
        }
    }
}
