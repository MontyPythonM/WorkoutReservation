using NLog;
using NLog.Web;
using WorkoutReservation.API.Middleware;
using WorkoutReservation.Application;
using WorkoutReservation.Domain.Common;
using WorkoutReservation.Infrastructure;
using WorkoutReservation.Infrastructure.Presistence;
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

    var firstAdminSettings = new FirstAdminSettings();
    builder.Configuration.GetSection("FirstAdmin").Bind(firstAdminSettings);
    builder.Services.AddSingleton(firstAdminSettings);

    builder.Services.AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
        .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

    builder.Services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());

    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices(builder.Configuration);

    builder.Services.AddScoped<ExceptionHandlingMiddleware>();

    var app = builder.Build();

    using var scope = app.Services.CreateScope();

    var firstAdminSeeder = scope.ServiceProvider.GetService<SeedFirstAdmin>(); 
    var dummyDataSeeder = scope.ServiceProvider.GetService<SeedDummyData>();

    var db = scope.ServiceProvider.GetService<AppDbContext>();

    // Configure the HTTP request pipeline.

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    
    firstAdminSeeder.Seed();
    dummyDataSeeder.Seed();

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
    logger.Fatal(ex, "The program has been stopped due to an exception.");
    throw;
}
finally
{
    LogManager.Shutdown();
}