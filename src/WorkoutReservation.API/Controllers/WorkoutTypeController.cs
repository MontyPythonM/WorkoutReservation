using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;

namespace WorkoutReservation.API.Controllers
{
    [ApiController]
    [Route("/api/workout-type")]
    public class WorkoutTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkoutTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkoutTypes()
        {
            var result = await _mediator.Send(new GetWorkoutTypesListQuery());
            return Ok(result);
        }

        [HttpGet("{workoutTypeId}")]
        public async Task<IActionResult> GetWorkoutType([FromRoute] int workoutTypeId)
        {
            var result = await _mediator.Send(new GetWorkoutTypeDetailQuery() { WorkoutTypeId = workoutTypeId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkoutType([FromBody] CreateWorkoutTypeCommand command)
        {
            var workoutTypeId = await _mediator.Send(command);
            return Created($"/api/workout-type/{workoutTypeId}", null);
        }

        [HttpDelete("{workoutTypeId}")]
        public async Task<IActionResult> DeleteWorkoutType([FromRoute] int workoutTypeId)
        {
            await _mediator.Send(new DeleteWorkoutTypeCommand() { WorkoutTypeId = workoutTypeId });
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkoutType([FromBody] UpdateWorkoutTypeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
