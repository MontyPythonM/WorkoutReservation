using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;

public record UpdateRepetitiveWorkoutCommand(int Id, int MaxParticipantNumber, TimeOnly StartTime, 
    TimeOnly EndTime, int InstructorId, int WorkoutTypeId, DayOfWeek DayOfWeek) : IRequest;

internal sealed class UpdateRepetitiveWorkoutCommandHandler : IRequestHandler<UpdateRepetitiveWorkoutCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;

    public UpdateRepetitiveWorkoutCommandHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
        IInstructorRepository instructorRepository,
        IWorkoutTypeRepository workoutTypeRepository)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _instructorRepository = instructorRepository; 
        _workoutTypeRepository = workoutTypeRepository;
    }
    
    public async Task<Unit> Handle(UpdateRepetitiveWorkoutCommand request, CancellationToken token)
    {
        var repetitiveWorkout = await _repetitiveWorkoutRepository
            .GetByIdAsync(request.Id, false, token, 
                incl => incl.Instructor, incl => incl.WorkoutType);
        
        if (repetitiveWorkout is null)
            throw new NotFoundException(nameof(RepetitiveWorkout), request.Id.ToString());

        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException(nameof(Instructor), request.InstructorId.ToString());

        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId, false, token);
        if (workoutType is null)
            throw new NotFoundException(nameof(WorkoutType), request.WorkoutTypeId.ToString());
        
        var dailyWorkoutsList = await _repetitiveWorkoutRepository
            .GetAllFromSelectedDayAsync(request.DayOfWeek, true, token);
        
        var validator = new UpdateRepetitiveWorkoutCommandValidator(repetitiveWorkout, dailyWorkoutsList);
        await validator.ValidateAndThrowAsync(request, token);

        repetitiveWorkout.Update(request.MaxParticipantNumber, request.StartTime, 
            request.EndTime, request.DayOfWeek, workoutType, instructor);

        await _repetitiveWorkoutRepository.UpdateAsync(repetitiveWorkout, token);
        return Unit.Value;
    }
}
