using WorkoutReservation.API;
using WorkoutReservation.API.Settings;
using WorkoutReservation.Application;
using WorkoutReservation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddNLog();
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddSwagger()
    .AddCorsPolicy(builder.Configuration)
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

var app = builder.Build();
var corsSettings = builder.Configuration.GetOptions<CorsSettings>(CorsSettings.SectionName);

app.UseCors(corsSettings.PolicyName);
app.UseInfrastructure();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkoutReservation REST API"));
}

app.Run();