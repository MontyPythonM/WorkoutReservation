using WorkoutReservation.API.Middleware;
using WorkoutReservation.Application;
using WorkoutReservation.Infrastructure;
using WorkoutReservation.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

app.MapControllers();

app.Run();
