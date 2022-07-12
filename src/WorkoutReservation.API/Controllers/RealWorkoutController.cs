using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;

namespace WorkoutReservation.API.Controllers;

[Authorize(Roles = "Manager, Administrator")]
[Route("api/real-workout/")]
public class RealWorkoutController : ApiControllerBase
{
    [HttpGet("current-week")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRealWorkoutsFromCurrentWeek()
    {
        var realWorkouts = await Mediator.Send(new GetRealWorkoutFromCurrentWeekQuery());
        return Ok(realWorkouts);
    }

    [HttpGet("{realWorkoutId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRealWorkoutById([FromRoute] int realWorkoutId)
    {
        var realWorkout = await Mediator.Send(new GetRealWorkoutDetailQuery { RealWorkoutId = realWorkoutId });
        return Ok(realWorkout);
    }

    [HttpGet("upcoming-week")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRealWorkoutsFromUpcomingWeek()
    {
        var realWorkouts = await Mediator.Send(new GetRealWorkoutFromUpcomingWeekQuery());
        return Ok(realWorkouts);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRealWorkout([FromBody] CreateRealWorkoutCommand command)
    {
        var realWorkoutId = await Mediator.Send(command);
        return Created($"/api/real-workout/{realWorkoutId}", null);
    }

    [HttpDelete("{realWorkoutId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRealWorkout([FromRoute] int realWorkoutId)
    {
        await Mediator.Send(new DeleteRealWorkoutCommand() { RealWorkoutId = realWorkoutId });
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRealWorkout([FromBody] UpdateRealWorkoutCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
