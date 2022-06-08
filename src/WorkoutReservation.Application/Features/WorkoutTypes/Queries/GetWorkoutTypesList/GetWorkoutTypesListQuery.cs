using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList
{
    public class GetWorkoutTypesListQuery : IRequest<List<WorkoutTypesListDto>>
    {

    }

    public class GetWorkoutTypesListQueryHandler : IRequestHandler<GetWorkoutTypesListQuery, List<WorkoutTypesListDto>>
    {
        private readonly IWorkoutTypeRepository _workoutTypeRepository;

        public GetWorkoutTypesListQueryHandler(IWorkoutTypeRepository workoutTypeRepository)
        {
            _workoutTypeRepository = workoutTypeRepository;
        }

        public async Task<List<WorkoutTypesListDto>> Handle(GetWorkoutTypesListQuery request, CancellationToken cancellationToken)
        {
            var workoutTypes = await _workoutTypeRepository.GetAllAsync();

            if (!workoutTypes.Any())
                throw new NotFoundException($"Workout types not found.");

            var results = workoutTypes
                .Select(x => new WorkoutTypesListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Intensity = x.Intensity
                })
                .ToList();

            return results;
        }
    }
}
