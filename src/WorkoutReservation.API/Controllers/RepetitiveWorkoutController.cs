using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteAllRepetitiveWorkouts;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;

namespace WorkoutReservation.API.Controllers
{
    [ApiController]
    [Route("/api/repetitive-workout/")]
    public class RepetitiveWorkoutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RepetitiveWorkoutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllRepetitiveWorkoutsPlan()
        {
            var result = await _mediator.Send(new GetRepetitiveWorkoutListQuery());
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRepetitiveWorkout([FromBody] CreateRepetitiveWorkoutCommand command)
        {
            var repetitiveWorkoutId = await _mediator.Send(command);
            return Created($"/api/repetitive-workout/{repetitiveWorkoutId}", null);
        }

        [HttpDelete("{repetitiveWorkoutId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRepetitiveWorkout([FromRoute] int repetitiveWorkoutId)
        {
            await _mediator.Send(new DeleteRepetitiveWorkoutCommand() { RepetitiveWorkoutId = repetitiveWorkoutId });
            return NoContent();
        }

        [HttpDelete("delete-all")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAllRepetitiveWorkout()
        {
            await _mediator.Send(new DeleteAllRepetitiveWorkoutsCommand());
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRepetitiveWorkout([FromBody] UpdateRepetitiveWorkoutCommand command)
        {
            await _mediator.Send(command); 
            return Ok();
        }
    }
}
