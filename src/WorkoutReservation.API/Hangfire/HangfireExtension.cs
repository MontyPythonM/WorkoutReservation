using Hangfire;
using MediatR;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;

namespace WorkoutReservation.API.Hangfire;

internal sealed class HangfireExtension
{
    private const string EveryMondayAtMidnight = "0 0 * * 1";
    private const string RecurringJobId = "GenerateUpcomingWorkoutJob";
    private readonly IMediator _mediator;
    private readonly ILogger<HangfireExtension> _logger;

    public HangfireExtension(IMediator mediator, ILogger<HangfireExtension> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task SendGenerateCommand(GenerateUpcomingWorkoutTimetableCommand command, CancellationToken token)
    {
        await _mediator.Send(command, token);
        _logger.LogInformation("GenerateUpcomingWorkoutTimetableCommand was send");
    }
    
    public static void AddGenerateWorkoutsRecurringJob(CancellationToken token = default)
    {
        RecurringJob.AddOrUpdate<HangfireExtension>(RecurringJobId, job => 
            job.SendGenerateCommand(new GenerateUpcomingWorkoutTimetableCommand(null), token), EveryMondayAtMidnight, TimeZoneInfo.Local); 
    }
    
    public static void EnqueueGenerateWorkoutsJob(GenerateUpcomingWorkoutTimetableCommand command, CancellationToken token = default) => 
        BackgroundJob.Enqueue<HangfireExtension>(job => job.SendGenerateCommand(command, token));
}