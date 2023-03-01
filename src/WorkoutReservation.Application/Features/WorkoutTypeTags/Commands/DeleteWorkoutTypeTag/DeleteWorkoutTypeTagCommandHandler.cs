using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.DeleteWorkoutTypeTag;

public record DeleteWorkoutTypeTagCommand(int Id) : IRequest;

internal sealed class DeleteWorkoutTypeTagCommandHandler : IRequestHandler<DeleteWorkoutTypeTagCommand>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;

    public DeleteWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
    }
    
    public async Task<Unit> Handle(DeleteWorkoutTypeTagCommand request, CancellationToken token)
    {
        var workoutTypeTag = await _workoutTypeTagRepository.GetByIdAsync(request.Id, false, token);

        if (workoutTypeTag is null)
            throw new NotFoundException(nameof(WorkoutTypeTag), request.Id.ToString());

        await _workoutTypeTagRepository.DeleteAsync(workoutTypeTag, token);
        return Unit.Value;
    }
}