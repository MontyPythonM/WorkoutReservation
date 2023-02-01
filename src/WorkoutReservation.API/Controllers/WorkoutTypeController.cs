using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/workout-type/")]
public class WorkoutTypeController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns paged list of workout types")]
    public async Task<IActionResult> GetAllWorkoutTypes([FromQuery] GetWorkoutTypesListQuery query, CancellationToken token)
    {
        return Ok(await Mediator.Send(query, token));
    }
    
    [HttpPost]
    [HasPermission(Permission.CreateWorkoutType)]
    [SwaggerOperation(Summary = "Create new workout type")]
    public async Task<IActionResult> CreateWorkoutType([FromBody] CreateWorkoutTypeCommand command, CancellationToken token)
    {
        var workoutTypeId = await Mediator.Send(command, token);
        return Created($"/api/workout-type/{workoutTypeId}", null);
    }

    [HttpDelete("{workoutTypeId}")]
    [HasPermission(Permission.DeleteWorkoutType)]
    [SwaggerOperation(Summary = "Delete selected workout type")]
    public async Task<IActionResult> DeleteWorkoutType([FromRoute] int workoutTypeId, CancellationToken token)
    {
        await Mediator.Send(new DeleteWorkoutTypeCommand(workoutTypeId), token);
        return NoContent();
    }

    [HttpPut]
    [HasPermission(Permission.UpdateWorkoutType)]
    [SwaggerOperation(Summary = "Update selected workout type")]
    public async Task<IActionResult> UpdateWorkoutType([FromBody] UpdateWorkoutTypeCommand command, CancellationToken token)
    {
        await Mediator.Send(command, token);
        return Ok();
    }
}
