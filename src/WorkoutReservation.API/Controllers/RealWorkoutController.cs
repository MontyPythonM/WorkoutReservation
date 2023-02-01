using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/real-workout/")]
public class RealWorkoutController : ApiControllerBase
{
    [HttpGet("current-week")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns a list of workouts for current week")]
    public async Task<IActionResult> GetRealWorkoutsFromCurrentWeek(CancellationToken token)
    {
        var realWorkouts = await Mediator.Send(new GetRealWorkoutFromCurrentWeekQuery(), token);
        return Ok(realWorkouts);
    }

    [HttpGet("upcoming-week")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns a list of workouts for upcoming week")]
    public async Task<IActionResult> GetRealWorkoutsFromUpcomingWeek(CancellationToken token)
    {
        var realWorkouts = await Mediator.Send(new GetRealWorkoutFromUpcomingWeekQuery(), token);
        return Ok(realWorkouts);
    }
    
    [HttpGet("{realWorkoutId}")]
    [HasPermission(Permission.GetRealWorkoutDetails)]
    [SwaggerOperation(Summary = "Returns selected workout")]
    public async Task<IActionResult> GetRealWorkoutDetails([FromRoute] int realWorkoutId, CancellationToken token)
    {
        var realWorkout = await Mediator.Send(new GetRealWorkoutDetailQuery(realWorkoutId), token);
        return Ok(realWorkout);
    }

    [HttpPost]   
    [HasPermission(Permission.CreateRealWorkout)]
    [SwaggerOperation(Summary = "Create new workout")]
    public async Task<IActionResult> CreateRealWorkout([FromBody] CreateRealWorkoutCommand command, CancellationToken token)
    {
        var realWorkoutId = await Mediator.Send(command, token);
        return Created($"/api/real-workout/{realWorkoutId}", null);
    }

    [HttpDelete("{realWorkoutId}")]
    [HasPermission(Permission.DeleteRealWorkout)]
    [SwaggerOperation(Summary = "Delete selected workout")]
    public async Task<IActionResult> DeleteRealWorkout([FromRoute] int realWorkoutId, CancellationToken token)
    {
        await Mediator.Send(new DeleteRealWorkoutCommand(realWorkoutId), token);
        return NoContent();
    }

    [HttpPut]
    [HasPermission(Permission.UpdateRealWorkout)]
    [SwaggerOperation(Summary = "Update selected workout")]
    public async Task<IActionResult> UpdateRealWorkout([FromBody] UpdateRealWorkoutCommand command, CancellationToken token)
    {
        await Mediator.Send(command, token);
        return Ok();
    }
}
