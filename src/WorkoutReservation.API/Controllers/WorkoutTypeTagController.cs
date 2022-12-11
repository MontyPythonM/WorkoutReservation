using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.DeactivateWorkoutTypeTag;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTagsList;

namespace WorkoutReservation.API.Controllers;

//[Authorize(Roles = "Administrator")]
[Route("api/workout-type-tag/")]
public class WorkoutTypeTagController : ApiControllerBase
{
    [HttpGet("only-active")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns list of active workout type tags")]
    public async Task<IActionResult> GetActiveWorkoutTypeTags(CancellationToken token)
    {
        var result = await Mediator.Send(new GetWorkoutTypeTagsListQuery() { OnlyActive = true }, token);
        return Ok(result);
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Returns list of workout type tags")]
    public async Task<IActionResult> GetWorkoutTypeTags(CancellationToken token)
    {
        var result = await Mediator.Send(new GetWorkoutTypeTagsListQuery() { OnlyActive = false }, token);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new workout type tag")]
    public async Task<IActionResult> CreateWorkoutTypeTag([FromBody] CreateWorkoutTypeTagCommand command, CancellationToken token)
    {        
        var workoutTypeTagId = await Mediator.Send(command, token);
        return Created($"Workout type tag with Id: {workoutTypeTagId} was created", null);
    }

    [HttpPatch("{workoutTypeTagId}")]
    [SwaggerOperation(Summary = "Deactivate selected workout type tag")]
    public async Task<IActionResult> DeactivateWorkoutTypeTag([FromRoute] int workoutTypeTagId, CancellationToken token)
    {
        await Mediator.Send(new DeactivateWorkoutTypeTagCommand() { Id = workoutTypeTagId }, token);
        return NoContent();
    }
}