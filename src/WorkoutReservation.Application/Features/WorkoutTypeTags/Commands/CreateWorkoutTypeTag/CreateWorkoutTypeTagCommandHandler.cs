using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;

public class CreateWorkoutTypeTagCommandHandler : IRequestHandler<CreateWorkoutTypeTagCommand, int>
{
    private readonly IWorkoutTypeTagRepository _workoutTypeTagRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateWorkoutTypeTagCommandHandler(IWorkoutTypeTagRepository workoutTypeTagRepository, 
        ICurrentUserService currentUserService)
    {
        _workoutTypeTagRepository = workoutTypeTagRepository;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(CreateWorkoutTypeTagCommand request, CancellationToken token)
    {
        var currentUser = _currentUserService.UserId.ToGuid();
        var workoutTypeTag = new WorkoutTypeTag(request.Tag, currentUser);
        
        workoutTypeTag = await _workoutTypeTagRepository.AddAsync(workoutTypeTag, token);
        return workoutTypeTag.Id;
    }
}