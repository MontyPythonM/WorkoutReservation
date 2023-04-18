using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

public record GenerateUpcomingWorkoutTimetableCommand(Guid? UserId) : IRequest;

internal sealed class GenerateUpcomingWorkoutTimetableCommandHandler : IRequestHandler<GenerateUpcomingWorkoutTimetableCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GenerateUpcomingWorkoutTimetableCommandHandler> _logger;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public GenerateUpcomingWorkoutTimetableCommandHandler(IRepetitiveWorkoutRepository repetitiveRepository,
        IRealWorkoutRepository realWorkoutRepository, IMapper mapper, ILogger<GenerateUpcomingWorkoutTimetableCommandHandler> logger, 
        IInstructorRepository instructorRepository, IWorkoutTypeRepository workoutTypeRepository, IDateTimeProvider dateTimeProvider)
    {
        _repetitiveRepository = repetitiveRepository;
        _realWorkoutRepository = realWorkoutRepository;
        _mapper = mapper;
        _logger = logger;
        _instructorRepository = instructorRepository;
        _workoutTypeRepository = workoutTypeRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(GenerateUpcomingWorkoutTimetableCommand request, CancellationToken token)
    {
        var repetitiveWorkouts = await _repetitiveRepository
            .GetAllAsync(false, token, incl => incl.Instructor, incl => incl.WorkoutType);
        
        if (!repetitiveWorkouts.Any())
        {
            _logger.LogWarning("Repetitive workouts not found. No new workout week has been generated.");
            throw new NotFoundException("Repetitive workouts not found.");
        }
        var convertedWorkouts = _mapper.Map<List<RepetitiveWorkoutToRealWorkoutDto>>(repetitiveWorkouts);
        
        convertedWorkouts.ForEach(workout => 
            workout.Date = _dateTimeProvider.CalculateDateInUpcomingWeek(workout.DayOfWeek));

        var workoutTypes = await _workoutTypeRepository.GetAllAsync(false, token);
        var instructors = await _instructorRepository.GetAllAsync(false, token);
        
        var newRealWorkouts = convertedWorkouts
            .Select(r => new RealWorkout(r.MaxParticipantNumber, r.StartTime, r.EndTime, 
                workoutTypes.FirstOrDefault(w => w.Id == r.WorkoutTypeId), 
                instructors.FirstOrDefault(i => i.Id == r.InstructorId), r.Date, true))
            .ToList();
        
        var existingRealWorkouts = await _realWorkoutRepository
            .GetAllFromDateRangeAsync(_dateTimeProvider.GetFirstDayOfUpcomingWeek(), 
                _dateTimeProvider.GetLastDayOfUpcomingWeek(), true, token);
        
        var validator = new GenerateUpcomingWorkoutTimetableCommandValidator(newRealWorkouts, existingRealWorkouts);
        await validator.ValidateAndThrowAsync(request, token);

        await _realWorkoutRepository.AddAsync(newRealWorkouts, token);
        _logger.LogInformation("Real workouts successfully generated");
        return Unit.Value;
    }
}
