using NLog;
using NLog.Web;
using WorkoutReservation.API.Middleware;
using WorkoutReservation.Application;
using WorkoutReservation.Infrastructure;
using WorkoutReservation.Infrastructure.Seeders;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Initialize a main function");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices(builder.Configuration);

    builder.Services.AddScoped<Seeder>();
    builder.Services.AddScoped<ExceptionHandlingMiddleware>();


    var app = builder.Build();


    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetService<Seeder>();


    // Configure the HTTP request pipeline.

    seeder.Seed();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    //app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Reservation REST API app");
    });

    app.MapControllers();

    logger.Debug("Application run");
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "The program has been stopped due to an exception.");
    throw;
}
finally
{
    LogManager.Shutdown();
}