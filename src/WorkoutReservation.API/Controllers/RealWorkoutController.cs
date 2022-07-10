using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;

namespace WorkoutReservation.API.Controllers;

[ApiController]
[Authorize(Roles = "Manager, Administrator")]
[Route("/api/real-workout/")]
public class RealWorkoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public RealWorkoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("current-week")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRealWorkoutsFromCurrentWeek()
    {
        var realWorkouts = await _mediator.Send(new GetRealWorkoutFromCurrentWeekQuery());
        return Ok(realWorkouts);
    }

    [HttpGet("{realWorkoutId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRealWorkoutById([FromRoute] int realWorkoutId)
    {
        var realWorkout = await _mediator.Send(new GetRealWorkoutDetailQuery { RealWorkoutId = realWorkoutId });
        return Ok(realWorkout);
    }

    [HttpGet("upcoming-week")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRealWorkoutsFromUpcomingWeek()
    {
        var realWorkouts = await _mediator.Send(new GetRealWorkoutFromUpcomingWeekQuery());
        return Ok(realWorkouts);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRealWorkout([FromBody] CreateRealWorkoutCommand command)
    {
        var realWorkoutId = await _mediator.Send(command);
        return Created($"/api/real-workout/{realWorkoutId}", null);
    }

    [HttpDelete("{realWorkoutId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRealWorkout([FromRoute] int realWorkoutId)
    {
        await _mediator.Send(new DeleteRealWorkoutCommand() { RealWorkoutId = realWorkoutId });
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRealWorkout([FromBody] UpdateRealWorkoutCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
