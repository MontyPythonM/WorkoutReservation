using System.ComponentModel;
using Hangfire;
using MediatR;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

namespace WorkoutReservation.Infrastructure.Hangfire;

public sealed class HangfireBackgroundJobsService
{
    private const string GenerateUpcomingWorkoutJob = "GenerateUpcomingWorkoutJob";
    private const string EveryMondayAtMidnight = "0 0 * * 1";
    
    private readonly IMediator _mediator;

    public HangfireBackgroundJobsService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// DisplayName attribute will be used by Hangfire to show in the UI Dashboard the name of the job
    /// </summary>
    [DisplayName("{0}")]
    public async Task Send<TCommand>(string jobName, TCommand command)
    {
        await _mediator.Send(command, CancellationToken.None);
    }
    
    public static void AddRecurringJob(CancellationToken token = default)
    {
        RecurringJob.AddOrUpdate<HangfireBackgroundJobsService>(GenerateUpcomingWorkoutJob, job => 
            job.Send(GenerateUpcomingWorkoutJob, new GenerateUpcomingWorkoutsCommand(null)), EveryMondayAtMidnight);
    }

    public static void EnqueueGenerateWorkoutsJob(GenerateUpcomingWorkoutsCommand command) => 
        BackgroundJob.Enqueue<HangfireBackgroundJobsService>(job => job.Send(GenerateUpcomingWorkoutJob, command));
}