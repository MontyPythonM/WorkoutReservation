using Microsoft.Extensions.DependencyInjection;
using Quartz;
using WorkoutReservation.Infrastructure.Messages;

namespace WorkoutReservation.Infrastructure.BackgroundJobs;

public static class Extensions
{
    internal static IServiceCollection AddQuartzBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(configuration =>
        {
            var messagesExecutorJobKey = new JobKey(nameof(MessagesExecutor));
            var generateUpcomingWorkoutsJobKey = new JobKey(nameof(GenerateUpcomingWorkoutsExecutor));
            
            configuration.AddJob<MessagesExecutor>(messagesExecutorJobKey)
                .AddTrigger(trigger => trigger.ForJob(messagesExecutorJobKey).WithSimpleSchedule(schedule => 
                    schedule.WithIntervalInSeconds(5).RepeatForever()));
            
            configuration.AddJob<GenerateUpcomingWorkoutsExecutor>(generateUpcomingWorkoutsJobKey)
                .AddTrigger(trigger => trigger.ForJob(generateUpcomingWorkoutsJobKey)
                    .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Monday, 0, 0)));
            
            configuration.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        return services;
    }
}