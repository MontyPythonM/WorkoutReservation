﻿using Hangfire;
using MediatR;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

namespace WorkoutReservation.API.Services;

internal sealed class HangfireExtension
{
    private const string EveryMondayAtMidnight = "0 0 * * 1";
    private const string RecurringJobId = "GenerateUpcomingWorkoutJob";
    private readonly IMediator _mediator;
    public HangfireExtension(IMediator mediator)
    {
        _mediator = mediator;
    }

    public static void GenerateUpcomingWeekWorkouts()
    {
        var command = new GenerateUpcomingWorkoutTimetableCommand { IsAutoGenerated = false };
        BackgroundJob.Enqueue<HangfireExtension>(job => job.Send(command));
    }

    public static void AddGenerateUpcomingWorkoutsRecurringJob()
    {
        var command = new GenerateUpcomingWorkoutTimetableCommand { IsAutoGenerated = true };
        RecurringJob.AddOrUpdate<HangfireExtension>(RecurringJobId, job => job
            .Send(command), EveryMondayAtMidnight, TimeZoneInfo.Local);
    }

    public async Task Send<T>(T command) => await _mediator.Send(command);
}