using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;

public record CreateRepetitiveWorkoutCommand(int MaxParticipantNumber, TimeOnly StartTime, TimeOnly EndTime, 
    int WorkoutTypeId, int InstructorId, DayOfWeek DayOfWeek) : IRequest;

internal sealed class CreateRepetitiveWorkoutCommandHandler : IRequestHandler<CreateRepetitiveWorkoutCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;

    public CreateRepetitiveWorkoutCommandHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
        IInstructorRepository instructorRepository,
        IWorkoutTypeRepository workoutTypeRepository)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _instructorRepository = instructorRepository;
        _workoutTypeRepository = workoutTypeRepository;
    }

    public async Task<Unit> Handle(CreateRepetitiveWorkoutCommand request, CancellationToken token)
    {
        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException(nameof(Instructor), request.InstructorId.ToString());

        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId, false, token);
        if (workoutType is null)
            throw new NotFoundException(nameof(WorkoutType), request.WorkoutTypeId.ToString());

        var dailyWorkoutsList = await _repetitiveWorkoutRepository
            .GetAllFromSelectedDayAsync(request.DayOfWeek, true, token);
        
        var validator = new CreateRepetitiveWorkoutCommandValidator(dailyWorkoutsList);
        await validator.ValidateAndThrowAsync(request, token);

        var repetitiveWorkout = new RepetitiveWorkout(request.MaxParticipantNumber, request.StartTime, 
            request.EndTime, request.DayOfWeek, workoutType, instructor);
        
        await _repetitiveWorkoutRepository.AddAsync(repetitiveWorkout, token);
        return Unit.Value;
    }
}
