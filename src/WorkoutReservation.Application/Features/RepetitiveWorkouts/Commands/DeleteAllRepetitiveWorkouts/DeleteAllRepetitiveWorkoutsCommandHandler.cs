using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteAllRepetitiveWorkouts;

public record DeleteAllRepetitiveWorkoutsCommand : IRequest;

internal sealed class DeleteAllRepetitiveWorkoutsCommandHandler : IRequestHandler<DeleteAllRepetitiveWorkoutsCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly ILogger<DeleteAllRepetitiveWorkoutsCommandHandler> _logger;

    public DeleteAllRepetitiveWorkoutsCommandHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
        ILogger<DeleteAllRepetitiveWorkoutsCommandHandler> logger)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteAllRepetitiveWorkoutsCommand request, CancellationToken token)
    {
        var repetitiveWorkouts = await _repetitiveWorkoutRepository.GetAllAsync(false, token);

        if(!repetitiveWorkouts.Any())
            throw new NotFoundException("Repetitive workouts not found.");

        await _repetitiveWorkoutRepository.DeleteAsync(repetitiveWorkouts, token);

        _logger.LogWarning($"Repetitive workouts [{repetitiveWorkouts.ToList().Count()} records] was deleted.");
        return Unit.Value;
    }
}
