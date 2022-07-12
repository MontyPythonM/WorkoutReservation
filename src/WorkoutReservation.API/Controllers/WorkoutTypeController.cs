using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;

namespace WorkoutReservation.API.Controllers;

[Authorize(Roles = "Manager, Administrator")]
[Route("api/workout-type/")]
public class WorkoutTypeController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllWorkoutTypes([FromQuery] GetWorkoutTypesListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{workoutTypeId}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkoutType([FromRoute] int workoutTypeId)
    {
        var result = await Mediator.Send(new GetWorkoutTypeDetailQuery() { WorkoutTypeId = workoutTypeId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]      
    public async Task<IActionResult> CreateWorkoutType([FromBody] CreateWorkoutTypeCommand command)
    {
        var workoutTypeId = await Mediator.Send(command);
        return Created($"/api/workout-type/{workoutTypeId}", null);
    }

    [HttpDelete("{workoutTypeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWorkoutType([FromRoute] int workoutTypeId)
    {
        await Mediator.Send(new DeleteWorkoutTypeCommand() { WorkoutTypeId = workoutTypeId });
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWorkoutType([FromBody] UpdateWorkoutTypeCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
