using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Infrastructure.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>()
            .AddScoped<IApplicationUserRepository, ApplicationUserRepository>()
            .AddScoped<IInstructorRepository, InstructorRepository>()
            .AddScoped<IWorkoutTypeRepository, WorkoutTypeRepository>()
            .AddScoped<IRepetitiveWorkoutRepository, RepetitiveWorkoutRepository>()
            .AddScoped<IRealWorkoutRepository, RealWorkoutRepository>()
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IWorkoutTypeTagRepository, WorkoutTypeTagRepository>()
            .AddScoped<IContentRepository, ContentRepository>();
        
        return services;
    }

    public static IQueryable<T> ApplyAsNoTracking<T>(this IQueryable<T> query, bool asNoTracking) 
        where T : class 
        => asNoTracking ? query.AsNoTracking() : query;

    public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, Expression<Func<T, object>>[] includes)
        where T : class 
        => includes.Any() ? includes.Aggregate(query, (current, include) => current.Include(include)) : query;
}