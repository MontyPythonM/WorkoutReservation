using AutoMapper;
using FluentValidation;
using MediatR;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;

public record CreateRepetitiveWorkoutCommand(int MaxParticipantNumber, TimeOnly StartTime, TimeOnly EndTime, 
    int WorkoutTypeId, int InstructorId, DayOfWeek DayOfWeek) : IRequest;

internal sealed class CreateRepetitiveWorkoutCommandHandler : IRequestHandler<CreateRepetitiveWorkoutCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveWorkoutRepository;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IMapper _mapper;

    public CreateRepetitiveWorkoutCommandHandler(IRepetitiveWorkoutRepository repetitiveWorkoutRepository,
        IInstructorRepository instructorRepository,
        IWorkoutTypeRepository workoutTypeRepository,
        IMapper mapper)
    {
        _repetitiveWorkoutRepository = repetitiveWorkoutRepository;
        _instructorRepository = instructorRepository;
        _workoutTypeRepository = workoutTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateRepetitiveWorkoutCommand request, CancellationToken token)
    {
        var instructor = await _instructorRepository.GetByIdAsync(request.InstructorId, false, token);
        if (instructor is null)
            throw new NotFoundException($"Instructor with Id: {request.InstructorId} not found.");

        var workoutType = await _workoutTypeRepository.GetByIdAsync(request.WorkoutTypeId, true, token);
        if (workoutType is null)
            throw new NotFoundException($"Workout type with Id: {request.WorkoutTypeId} not found.");

        var dailyWorkoutsList = await _repetitiveWorkoutRepository
            .GetAllFromSelectedDayAsync(request.DayOfWeek, token);
        
        var validator = new CreateRepetitiveWorkoutCommandValidator(dailyWorkoutsList);
        await validator.ValidateAndThrowAsync(request, token);

        var repetitiveWorkout = _mapper.Map<RepetitiveWorkout>(request);
        repetitiveWorkout.CreatedDate = DateTime.Now;
        
        await _repetitiveWorkoutRepository.AddAsync(repetitiveWorkout, token);
        return Unit.Value;
    }
}
