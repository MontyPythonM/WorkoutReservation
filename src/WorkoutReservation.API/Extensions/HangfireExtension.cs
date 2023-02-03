using Hangfire;
using MediatR;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

namespace WorkoutReservation.API.Extensions;

internal sealed class HangfireExtension
{
    private const string EveryMondayAtMidnight = "0 0 * * 1";
    private const string RecurringJobId = "GenerateUpcomingWorkoutJob";
    private readonly IMediator _mediator;
    
    public HangfireExtension(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Send(GenerateUpcomingWorkoutTimetableCommand command) => await _mediator.Send(command);

    public static void AddGenerateWorkoutsRecurringJob() =>
        RecurringJob.AddOrUpdate<HangfireExtension>(RecurringJobId, job => 
            job.Send(new GenerateUpcomingWorkoutTimetableCommand(null)), EveryMondayAtMidnight, TimeZoneInfo.Local);

    public static void EnqueueGenerateWorkoutsJob(GenerateUpcomingWorkoutTimetableCommand command) =>
        BackgroundJob.Enqueue<HangfireExtension>(job => job.Send(command));
}