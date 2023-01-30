using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.DeleteWorkoutTypeTag;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.UpdateWorkoutTypeTag;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetActiveWorkoutTypeTags;
using WorkoutReservation.Application.Features.WorkoutTypeTags.Queries.GetWorkoutTypeTags;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/workout-type-tag/")]
public class WorkoutTypeTagController : ApiControllerBase
{
    [HttpGet("only-active")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns list of active workout type tags")]
    public async Task<IActionResult> GetActiveWorkoutTypeTags(CancellationToken token)
    {
        var result = await Mediator.Send(new GetActiveWorkoutTypeTagsQuery(), token);
        return Ok(result);
    }
    
    [HttpGet]
    [HasPermission(Permission.GetAllWorkoutTypeTags)]
    [SwaggerOperation(Summary = "Returns list of workout type tags")]
    public async Task<IActionResult> GetWorkoutTypeTags(CancellationToken token)
    {
        var result = await Mediator.Send(new GetWorkoutTypeTagsQuery(), token);
        return Ok(result);
    }

    [HttpPost]
    [HasPermission(Permission.CreateWorkoutTypeTag)]
    [SwaggerOperation(Summary = "Create new workout type tag")]
    public async Task<IActionResult> CreateWorkoutTypeTag([FromBody] CreateWorkoutTypeTagCommand command, CancellationToken token)
    {        
        var workoutTypeTagId = await Mediator.Send(command, token);
        return Created($"Workout type tag with Id: {workoutTypeTagId} was created", null);
    }
    
    [HttpPut]
    [HasPermission(Permission.UpdateWorkoutTypeTag)]
    [SwaggerOperation(Summary = "Edit selected workout type tag")]
    public async Task<IActionResult> UpdateWorkoutTypeTag([FromBody] UpdateWorkoutTypeTagCommand command, CancellationToken token)
    {
        await Mediator.Send(command, token);
        return Ok();
    }
    
    [HttpDelete("{workoutTypeTagId}")]
    [HasPermission(Permission.DeleteWorkoutTypeTag)]
    [SwaggerOperation(Summary = "Delete selected workout type tag")]
    public async Task<IActionResult> DeleteWorkoutTypeTag([FromRoute] int workoutTypeTagId, CancellationToken token)
    {
        await Mediator.Send(new DeleteWorkoutTypeTagCommand() { Id = workoutTypeTagId }, token);
        return NoContent();
    }
}