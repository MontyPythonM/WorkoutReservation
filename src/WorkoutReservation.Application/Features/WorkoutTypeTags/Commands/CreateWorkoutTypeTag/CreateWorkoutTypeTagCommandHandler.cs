using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;

public record CreateWorkoutTypeTagCommand(string Tag) : IRequest<int>;

internal sealed class CreateWorkoutTypeTagCommandHandler : IRequestHandler<CreateWorkoutTypeTagCommand, int>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public CreateWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository, 
        ICurrentUserAccessor currentUserAccessor)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _currentUserAccessor = currentUserAccessor;
    }

    public async Task<int> Handle(CreateWorkoutTypeTagCommand request, CancellationToken token)
    {
        var workoutTypeTag = new WorkoutTypeTag(request.Tag, _currentUserAccessor.GetCurrentUserId());
        
        workoutTypeTag = await _workoutTypeTagRepository.AddAsync(workoutTypeTag, token);
        return workoutTypeTag.Id;
    }
}