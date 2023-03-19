using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteRepetitiveWorkout;

public record DeleteRepetitiveWorkoutCommand(int RepetitiveWorkoutId) : IRequest;

internal sealed class DeleteRepetitiveWorkoutCommandHandler : IRequestHandler<DeleteRepetitiveWorkoutCommand>
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
        CancellationToken token)
    {
        var repetitiveWorkout = await _repetitiveWorkoutRepository
            .GetByIdAsync(request.RepetitiveWorkoutId, false, token,
                incl => incl.Instructor, incl => incl.WorkoutType);

        if (repetitiveWorkout is null)
            throw new NotFoundException(nameof(RepetitiveWorkout), request.RepetitiveWorkoutId.ToString());

        await _repetitiveWorkoutRepository.DeleteAsync(repetitiveWorkout, token);

        _logger.LogInformation($"Repetitive workout with Id: {request.RepetitiveWorkoutId} was deleted.");
        return Unit.Value;
    }
}

