using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Infrastructure.Presistence;
using WorkoutReservation.Infrastructure.Repositories;

namespace WorkoutReservation.Infrastructure
{
    public static class ConfigureInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("localDbConnection")));

            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IWorkoutTypeRepository, WorkoutTypeRepository>();
            services.AddScoped<IRepetitiveWorkoutRepository, RepetitiveWorkoutRepository>();

            return services;
        }
    }
}
