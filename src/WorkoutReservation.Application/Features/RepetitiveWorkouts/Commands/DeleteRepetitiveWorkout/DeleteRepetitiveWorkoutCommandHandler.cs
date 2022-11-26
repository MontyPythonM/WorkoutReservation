using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteRepetitiveWorkout;

public class DeleteRepetitiveWorkoutCommandHandler : IRequestHandler<DeleteRepetitiveWorkoutCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly ILogger<DeleteRepetitiveWorkoutCommandHandler> _logger;

    public DeleteRepetitiveWorkoutCommandHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
                                                 ILogger<DeleteRepetitiveWorkoutCommandHandler> logger)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteRepetitiveWorkoutCommand request, 
                                   CancellationToken cancellationToken)
    {
        var repetitiveWorkout = await _repetitiveWorkoutRepository.GetByIdAsync(request.RepetitiveWorkoutId, cancellationToken);

        if (repetitiveWorkout is null)
            throw new NotFoundException($"Repetitive workout with Id: {request.RepetitiveWorkoutId} not found.");

        await _repetitiveWorkoutRepository.DeleteAsync(repetitiveWorkout, cancellationToken);

        _logger.LogInformation($"Repetitive workout with Id: {request.RepetitiveWorkoutId} was deleted.");

        return Unit.Value;
    }
}

