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
        return await SendAsync(new GetActiveWorkoutTypeTagsQuery(), token);
    }
    
    [HttpGet]
    [HasPermission(Permission.GetAllWorkoutTypeTags)]
    [SwaggerOperation(Summary = "Returns list of workout type tags")]
    public async Task<IActionResult> GetWorkoutTypeTags(CancellationToken token)
    {
        return await SendAsync(new GetWorkoutTypeTagsQuery(), token);
    }

    [HttpPost]
    [HasPermission(Permission.CreateWorkoutTypeTag)]
    [SwaggerOperation(Summary = "Create new workout type tag")]
    public async Task<IActionResult> CreateWorkoutTypeTag([FromBody] CreateWorkoutTypeTagCommand command, CancellationToken token)
    {        
        return await SendAsync(command, token);
    }
    
    [HttpPut]
    [HasPermission(Permission.UpdateWorkoutTypeTag)]
    [SwaggerOperation(Summary = "Edit selected workout type tag")]
    public async Task<IActionResult> UpdateWorkoutTypeTag([FromBody] UpdateWorkoutTypeTagCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
    
    [HttpDelete("{workoutTypeTagId}")]
    [HasPermission(Permission.DeleteWorkoutTypeTag)]
    [SwaggerOperation(Summary = "Delete selected workout type tag")]
    public async Task<IActionResult> DeleteWorkoutTypeTag([FromRoute] int workoutTypeTagId, CancellationToken token)
    {
        return await SendAsync(new DeleteWorkoutTypeTagCommand(workoutTypeTagId), token);
    }
}