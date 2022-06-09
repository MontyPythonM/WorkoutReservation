using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.DeleteInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;

namespace WorkoutReservation.API.Controllers
{
    [ApiController]
    [Route("/api/instructor")]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInstructors()
        {
            var result = await _mediator.Send(new GetInstructorListQuery());
            return Ok(result);
        }

        [HttpGet("{instructorId}")]
        public async Task<IActionResult> GetInstructor([FromRoute] int instructorId)
        {
            var result = await _mediator.Send(new GetInstructorDetailQuery() { InstructorId = instructorId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstructor([FromBody] CreateInstructorCommand command)
        {
            var instructorId = await _mediator.Send(command);
            return Created($"/api/instructor/{instructorId}", null);
        }

        [HttpDelete("{instructorId}")]
        public async Task<IActionResult> DeleteInstructor([FromRoute] int instructorId)
        {
            await _mediator.Send(new DeleteInstructorCommand() { InstructorId = instructorId });
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInstructor([FromBody] UpdateInstructorCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
