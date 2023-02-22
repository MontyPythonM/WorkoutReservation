using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/instructor/")]
public class InstructorController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns list of instructors")]
    public async Task<IActionResult> GetAllInstructors(CancellationToken token)
    {
        return await SendAsync(new GetInstructorListQuery(), token);
    }

    [HttpGet("{instructorId}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns selected instructor with list of related workout types")]
    public async Task<IActionResult> GetInstructor([FromRoute] int instructorId, CancellationToken token)
    {
        return await SendAsync(new GetInstructorDetailQuery(instructorId), token);
    }

    [HttpPost]
    [HasPermission(Permission.CreateInstructor)]
    [SwaggerOperation(Summary = "Create new instructor")]
    public async Task<IActionResult> CreateInstructor([FromBody] CreateInstructorCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }

    [HttpDelete("{instructorId}")]
    [HasPermission(Permission.DeleteInstructor)]
    [SwaggerOperation(Summary = "Delete selected instructor")]
    public async Task<IActionResult> DeleteInstructor([FromRoute] int instructorId, CancellationToken token)
    {
        return await SendAsync(new DeleteInstructorCommand(instructorId), token);
    }

    [HttpPut]
    [HasPermission(Permission.UpdateInstructor)]
    [SwaggerOperation(Summary = "Update selected instructor")]
    public async Task<IActionResult> UpdateInstructor([FromBody] UpdateInstructorCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
}
