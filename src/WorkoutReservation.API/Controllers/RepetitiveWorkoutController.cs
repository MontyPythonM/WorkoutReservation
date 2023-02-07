using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.API.Extensions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteAllRepetitiveWorkouts;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.GenerateUpcomingWorkoutTimetable;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/repetitive-workout/")]
public class RepetitiveWorkoutController : ApiControllerBase
{
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public RepetitiveWorkoutController(ICurrentUserAccessor currentUserAccessor)
    {
        _currentUserAccessor = currentUserAccessor;
    }
    
    [HttpGet]
    [HasPermission(Permission.GetRepetitiveWorkouts)]
    [SwaggerOperation(Summary = "Returns a list of repetitive workouts")]
    public async Task<IActionResult> GetAllRepetitiveWorkoutsPlan(CancellationToken token)
    {
        var result = await Mediator.Send(new GetRepetitiveWorkoutListQuery(), token);
        return Ok(result);
    }

    [HttpPost]
    [HasPermission(Permission.CreateRepetitiveWorkout)]
    [SwaggerOperation(Summary = "Create new repetitive workout")]
    public async Task<IActionResult> CreateRepetitiveWorkout([FromBody] CreateRepetitiveWorkoutCommand command, CancellationToken token)
    {
        var repetitiveWorkoutId = await Mediator.Send(command, token);
        return Created($"/api/repetitive-workout/{repetitiveWorkoutId}", null);
    }

    [HttpDelete("{repetitiveWorkoutId}")]
    [HasPermission(Permission.DeleteRepetitiveWorkout)]
    [SwaggerOperation(Summary = "Delete selected repetitive workout")]
    public async Task<IActionResult> DeleteRepetitiveWorkout([FromRoute] int repetitiveWorkoutId, CancellationToken token)
    {
        await Mediator.Send(new DeleteRepetitiveWorkoutCommand(repetitiveWorkoutId), token);
        return NoContent();
    }

    [HttpDelete("delete-all")]
    [HasPermission(Permission.DeleteAllRepetitiveWorkouts)]
    [SwaggerOperation(Summary = "Delete all repetitive workouts")]
    public async Task<IActionResult> DeleteAllRepetitiveWorkout(CancellationToken token)
    {
        await Mediator.Send(new DeleteAllRepetitiveWorkoutsCommand(), token);
        return NoContent();
    }

    [HttpPut]
    [HasPermission(Permission.UpdateRepetitiveWorkout)]
    [SwaggerOperation(Summary = "Update selected repetitive workout")]
    public async Task<IActionResult> UpdateRepetitiveWorkout([FromBody] UpdateRepetitiveWorkoutCommand command, CancellationToken token)
    {
        await Mediator.Send(command, token); 
        return Ok();
    }

    [HttpPost("generate-upcoming-week")]
    [HasPermission(Permission.GenerateNewUpcomingWeek)]
    [SwaggerOperation(Summary = "Forces generation of a new repetitive workouts list for the coming week")]
    public async Task<IActionResult> GenerateNewUpcomingWeek(CancellationToken token)
    {
        var command = new GenerateUpcomingWorkoutTimetableCommand(_currentUserAccessor.GetUserId());
        HangfireExtension.EnqueueGenerateWorkoutsJob(command);
        return Ok();
    }
}
