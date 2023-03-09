using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;

public record UpdateRealWorkoutCommand(int Id, int MaxParticipantNumber, DateOnly Date, 
    TimeOnly StartTime, TimeOnly EndTime, int InstructorId) : IRequest;

internal sealed class UpdateRealWorkoutCommandHandler : IRequestHandler<UpdateRealWorkoutCommand>
{
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;

    public UpdateRealWorkoutCommandHandler(IRealWorkoutRepository realWorkoutRepository,
        IInstructorRepository instructorRepository)
    {
        _realWorkoutRepository = realWorkoutRepository;
        _instructorRepository = instructorRepository;
    }

    public async Task<Unit> Handle(UpdateRealWorkoutCommand request, CancellationToken token)
    {
        var realWorkout = await _realWorkoutRepository.GetByIdAsync(request.Id, false, token);
        if (realWorkout is null)
            throw new NotFoundException($"Real workout with Id: {request.Id} not found.");

        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");
        
        var dailyWorkoutsList = await _realWorkoutRepository.GetByDayAsync(request.Date, true, token);
        
        var validator = new UpdateRealWorkoutCommandValidator(dailyWorkoutsList, realWorkout);
        await validator.ValidateAndThrowAsync(request, token);

        realWorkout.UpdateLastModifiedDate();
        realWorkout.MaxParticipantNumber = request.MaxParticipantNumber;
        realWorkout.Date = request.Date;
        realWorkout.StartTime = request.StartTime;
        realWorkout.EndTime = request.EndTime;
        realWorkout.Instructor = instructor;
        
        await _realWorkoutRepository.UpdateAsync(realWorkout, token);
        return Unit.Value;
    }
}
