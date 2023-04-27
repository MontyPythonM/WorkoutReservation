using Microsoft.Extensions.DependencyInjection;
using Quartz;
using WorkoutReservation.Infrastructure.Outbox;

namespace WorkoutReservation.Infrastructure.BackgroundJobs;

public static class QuartzExtensions
{
    internal static IServiceCollection AddQuartzBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(configuration =>
        {
            var jobKey = new JobKey(nameof(OutboxMessagesExecutor)); 
            
            configuration.AddJob<OutboxMessagesExecutor>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(schedule => 
                    schedule.WithIntervalInSeconds(5).RepeatForever()));
            
            configuration.UseMicrosoftDependencyInjectionJobFactory();
        });

        return services;
    }
}