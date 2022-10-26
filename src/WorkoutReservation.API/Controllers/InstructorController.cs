using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;

namespace WorkoutReservation.API.Controllers;

[Authorize(Roles = "Manager, Administrator")]
[Route("api/instructor/")]
public class InstructorController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns list of instructors")]
    public async Task<IActionResult> GetAllInstructors()
    {
        var result = await Mediator.Send(new GetInstructorListQuery());
        return Ok(result);
    }

    [HttpGet("{instructorId}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns selected instructor with list of related workout types")]
    public async Task<IActionResult> GetInstructor([FromRoute] int instructorId)
    {
        var result = await Mediator.Send(new GetInstructorDetailQuery() { InstructorId = instructorId });
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new instructor")]
    public async Task<IActionResult> CreateInstructor([FromBody] CreateInstructorCommand command)
    {
        var instructorId = await Mediator.Send(command);
        return Created($"/api/instructor/{instructorId}", null);
    }

    [HttpDelete("{instructorId}")]
    [SwaggerOperation(Summary = "Delete selected instructor")]
    public async Task<IActionResult> DeleteInstructor([FromRoute] int instructorId)
    {
        await Mediator.Send(new DeleteInstructorCommand() { InstructorId = instructorId });
        return NoContent();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update selected instructor")]
    public async Task<IActionResult> UpdateInstructor([FromBody] UpdateInstructorCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
