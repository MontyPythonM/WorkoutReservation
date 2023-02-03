using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using WorkoutReservation.API.Extensions;
using WorkoutReservation.API.Middleware;
using WorkoutReservation.Application;
using WorkoutReservation.Infrastructure;
using WorkoutReservation.Infrastructure.Authorization;
using WorkoutReservation.Infrastructure.Seeders;
using WorkoutReservation.Infrastructure.Settings;
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

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

    builder.Services.AddAuthorization();
    builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
    builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    
    var systemAdministratorSettings = new SystemAdministratorSettings();
    builder.Configuration.GetSection("FirstAdmin").Bind(systemAdministratorSettings);

    //--- Add services to the container
    builder.Services.AddSingleton(authenticationSettings);
    builder.Services.AddSingleton(systemAdministratorSettings);

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
    builder.Services.AddCors();

    //--- Build application
    var app = builder.Build();

    //--- Get service instances
    using var scope = app.Services.CreateScope();
    var systemAdministratorSeeder = scope.ServiceProvider.GetService<SystemAdministratorSeeder>(); 
    var applicationDataSeeder = scope.ServiceProvider.GetService<ApplicationDataSeeder>();

    //--- Configure the HTTP request pipeline
    await systemAdministratorSeeder!.SeedAsync(CancellationToken.None);
    await applicationDataSeeder!.SeedAsync(CancellationToken.None);

    if (app.Environment.IsDevelopment())
    {
        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Reservation REST API Application"));
    }
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    
    app.UseCors(policy => policy
        .AllowAnyHeader()
        .WithMethods("POST", "PUT", "PATCH", "DELETE", "UPDATE", "OPTIONS")
        .WithOrigins("http://localhost:4200")
        .AllowCredentials());
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[] { new HangfireAuthorizationFilter(app.Services) },
        IsReadOnlyFunc = _ => true
    });
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    
    HangfireExtension.AddGenerateWorkoutsRecurringJob();
    
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