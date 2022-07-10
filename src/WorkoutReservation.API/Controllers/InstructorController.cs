﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;

namespace WorkoutReservation.API.Controllers;

[ApiController]
[Authorize(Roles = "Manager, Administrator")]
[Route("/api/instructor/")]
public class InstructorController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstructorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllInstructors()
    {
        var result = await _mediator.Send(new GetInstructorListQuery());
        return Ok(result);
    }

    [HttpGet("{instructorId}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInstructor([FromRoute] int instructorId)
    {
        var result = await _mediator.Send(new GetInstructorDetailQuery() { InstructorId = instructorId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateInstructor([FromBody] CreateInstructorCommand command)
    {
        var instructorId = await _mediator.Send(command);
        return Created($"/api/instructor/{instructorId}", null);
    }

    [HttpDelete("{instructorId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteInstructor([FromRoute] int instructorId)
    {
        await _mediator.Send(new DeleteInstructorCommand() { InstructorId = instructorId });
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateInstructor([FromBody] UpdateInstructorCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
