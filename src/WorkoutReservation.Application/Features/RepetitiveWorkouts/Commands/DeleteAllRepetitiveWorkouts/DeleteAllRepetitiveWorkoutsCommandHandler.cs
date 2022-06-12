using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteAllRepetitiveWorkouts
{
    public class DeleteAllRepetitiveWorkoutsCommandHandler : IRequestHandler<DeleteAllRepetitiveWorkoutsCommand>
    {
        private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
        private readonly ILogger<DeleteAllRepetitiveWorkoutsCommandHandler> _logger;

        public DeleteAllRepetitiveWorkoutsCommandHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
                                                         ILogger<DeleteAllRepetitiveWorkoutsCommandHandler> logger)
        {
            _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteAllRepetitiveWorkoutsCommand request, 
                                       CancellationToken cancellationToken)
        {
            var repetitiveWorkouts = await _repetitiveWorkoutRepository.GetAllAsync();

            if(!repetitiveWorkouts.Any())
                throw new NotFoundException("Repetitive workouts not found.");

            await _repetitiveWorkoutRepository.DeleteAllAsync(repetitiveWorkouts);

            _logger.LogWarning($"Repetitive workouts [{repetitiveWorkouts.Count} records] was deleted.");

            return Unit.Value;
        }
    }
}
