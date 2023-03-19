using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;

public record CreateWorkoutTypeTagCommand(string Tag) : IRequest;

internal sealed class CreateWorkoutTypeTagCommandHandler : IRequestHandler<CreateWorkoutTypeTagCommand>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;

    public CreateWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
    }

    public async Task<Unit> Handle(CreateWorkoutTypeTagCommand request, CancellationToken token)
    {
        var workoutTypeTag = new WorkoutTypeTag(request.Tag);
        
        await _workoutTypeTagRepository.AddAsync(workoutTypeTag, token);
        return Unit.Value;
    }
}