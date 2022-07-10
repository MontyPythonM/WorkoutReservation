using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.API.Controllers;

[ApiController]
[Authorize(Roles = "Manager, Administrator")]
[Route("/api/workout-type/")]
public class WorkoutTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllWorkoutTypes([FromQuery] GetWorkoutTypesListQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{workoutTypeId}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkoutType([FromRoute] int workoutTypeId)
    {
        var result = await _mediator.Send(new GetWorkoutTypeDetailQuery() { WorkoutTypeId = workoutTypeId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]      
    public async Task<IActionResult> CreateWorkoutType([FromBody] CreateWorkoutTypeCommand command)
    {
        var workoutTypeId = await _mediator.Send(command);
        return Created($"/api/workout-type/{workoutTypeId}", null);
    }

    [HttpDelete("{workoutTypeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWorkoutType([FromRoute] int workoutTypeId)
    {
        await _mediator.Send(new DeleteWorkoutTypeCommand() { WorkoutTypeId = workoutTypeId });
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWorkoutType([FromBody] UpdateWorkoutTypeCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
