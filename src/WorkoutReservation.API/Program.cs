using Hangfire;
using Microsoft.Extensions.Options;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using WorkoutReservation.API.Filters;
using WorkoutReservation.API.Middleware;
using WorkoutReservation.API.Swagger;
using WorkoutReservation.Application;
using WorkoutReservation.Infrastructure;
using WorkoutReservation.Infrastructure.Hangfire;
using WorkoutReservation.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();
    
builder.Services.AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddScoped<ExceptionHandlingMiddleware>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("WorkoutReservationUIOrigin", policy =>
    {
        policy.WithOrigins(builder.Configuration["WorkoutReservationUIOrigin"])
            .AllowAnyHeader()
            .WithMethods("POST", "PUT", "PATCH", "DELETE", "UPDATE", "OPTIONS")
            .AllowCredentials();
    });
});
    
//--- Build application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkoutReservation REST API"));
        
    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider
        .GetService<SystemAdministratorSeeder>()
        .SeedAsync(); 
        
    await scope.ServiceProvider
        .GetService<ApplicationDataSeeder>()
        .SeedAsync();
    
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[] { new HangfireAuthorizationFilter(app.Services) },
        IsReadOnlyFunc = _ => false
    });
}

app.UseCors("WorkoutReservationUIOrigin");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

HangfireBackgroundJobsService.AddRecurringJob();
    
app.MapControllers();
app.Run();