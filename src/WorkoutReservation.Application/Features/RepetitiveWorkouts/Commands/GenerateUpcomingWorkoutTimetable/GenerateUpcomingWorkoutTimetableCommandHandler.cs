﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Methods;

namespace WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

public class GenerateUpcomingWorkoutTimetableCommandHandler : IRequestHandler<GenerateUpcomingWorkoutTimetableCommand>
{
    private readonly IRepetitiveWorkoutRepository _repetitiveRepository;
    private readonly IRealWorkoutRepository _realWorkoutRepository;
    private readonly ICurrentUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<GenerateUpcomingWorkoutTimetableCommandHandler> _logger;

    public GenerateUpcomingWorkoutTimetableCommandHandler(IRepetitiveWorkoutRepository repetitiveRepository,
        IRealWorkoutRepository realWorkoutRepository,
        ICurrentUserService userService,
        IMapper mapper,
        ILogger<GenerateUpcomingWorkoutTimetableCommandHandler> logger)
    {
        _repetitiveRepository = repetitiveRepository;
        _realWorkoutRepository = realWorkoutRepository;
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(GenerateUpcomingWorkoutTimetableCommand request, CancellationToken cancellationToken)
    {
        // get repetitive workouts timetable
        var repetitiveWorkouts = await _repetitiveRepository.GetAllAsync(cancellationToken);

        if (!repetitiveWorkouts.Any())
        {
            _logger.LogWarning("Repetitive workouts not found. No new workout week has been generated.");
            throw new NotFoundException("Repetitive workouts not found.");
        }

        // get first day (monday) of next week
        var firstDayOfUpcomingWeek = DateTime.Now.GetFirstDayOfWeek().AddDays(7);

        var convertedWorkouts = _mapper
            .Map<List<RepetitiveWorkoutToRealWorkoutDto>>(repetitiveWorkouts);
        
        foreach (var workout in convertedWorkouts)
        {
            workout.Date = workout.DayOfWeek switch
            {
                DayOfWeek.Monday => firstDayOfUpcomingWeek,
                DayOfWeek.Tuesday => firstDayOfUpcomingWeek.AddDays(1),
                DayOfWeek.Wednesday => firstDayOfUpcomingWeek.AddDays(2),
                DayOfWeek.Thursday => firstDayOfUpcomingWeek.AddDays(3),
                DayOfWeek.Friday => firstDayOfUpcomingWeek.AddDays(4),
                DayOfWeek.Saturday => firstDayOfUpcomingWeek.AddDays(5),
                DayOfWeek.Sunday => firstDayOfUpcomingWeek.AddDays(6),
                _ => workout.Date
            };
        }

        string createdBy = null;

        if (!request.IsAutoGenerated)
        {
            createdBy = Guid.Parse(_userService.UserId).ToString();
        }

        var newRealWorkouts = convertedWorkouts.Select(x => new RealWorkout
        {
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            InstructorId = x.InstructorId,
            WorkoutTypeId = x.WorkoutTypeId,
            MaxParticipantNumber = x.MaxParticipantNumber,
            Date = x.Date,
            CurrentParticipantNumber = 0,
            IsAutoGenerated = request.IsAutoGenerated,
            CreatedDate = DateTime.Now,
            CreatedBy = createdBy
        })
        .ToList();

        // validation
        var validator = new GenerateUpcomingWorkoutTimetableCommandValidator(newRealWorkouts);
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        // add new real workouts for upcoming week
        await _realWorkoutRepository.AddRangeAsync(newRealWorkouts, cancellationToken);

        _logger.LogInformation("The method generating a new weekly workout plan has been called");
        return Unit.Value;
    }
}
