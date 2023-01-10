using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using WorkoutReservation.API.Filters;
using WorkoutReservation.API.Middleware;
using WorkoutReservation.API.Services;
using WorkoutReservation.Application;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure;
using WorkoutReservation.Infrastructure.Seeders;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Initialize a main function");

try
{
    var builder = WebApplication.CreateBuilder(args);

    //--- NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();

    //--- JWT authentication settings configuration
    var authenticationSettings = new AuthenticationSettings();
    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
        option.DefaultScheme = "Bearer";
    }).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

    var firstAdminSettings = new FirstAdminSettings();
    builder.Configuration.GetSection("FirstAdmin").Bind(firstAdminSettings);

    //--- Add services to the container
    builder.Services.AddSingleton(authenticationSettings);
    builder.Services.AddSingleton(firstAdminSettings);
    builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
        .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

    builder.Services.AddSwaggerGen(c =>
    {
        c.UseDateOnlyTimeOnlyStringConverters();
        c.EnableAnnotations();
    });
    
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddScoped<ExceptionHandlingMiddleware>();
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    builder.Services.AddCors();

    //--- Build application
    var app = builder.Build();

    //--- Get service instances
    using var scope = app.Services.CreateScope();

    var firstAdminSeeder = scope.ServiceProvider.GetService<SeedFirstAdmin>(); 
    var dummyDataSeeder = scope.ServiceProvider.GetService<SeedDummyData>();

    //--- Configure the HTTP request pipeline
    await firstAdminSeeder!.SeedAsync(CancellationToken.None);
    await dummyDataSeeder!.SeedAsync(CancellationToken.None);

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Reservation REST API Application"));
    }
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    
    app.UseCors(policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200"));
    
    app.UseAuthentication();
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[] { new HangfireAuthorizationFilter() },
        IsReadOnlyFunc = _ => true
    });

    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    
    HangfireExtension.AddGenerateUpcomingWorkoutsRecurringJob();
    
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