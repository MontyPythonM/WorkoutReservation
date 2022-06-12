using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;

namespace WorkoutReservation.API.Controllers
{
    [ApiController]
    [Route("/api/real-workout/")]
    public class RealWorkoutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RealWorkoutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("current-week")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRealWorkoutsFromCurrentWeek()
        {
            var realWorkouts = await _mediator.Send(new GetRealWorkoutFromCurrentWeekQuery());
            return Ok(realWorkouts);
        }

        [HttpGet("{realWorkoutId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRealWorkoutById([FromRoute] int realWorkoutId)
        {
            var realWorkout = await _mediator.Send(new GetRealWorkoutDetailQuery { RealWorkoutId = realWorkoutId });
            return Ok(realWorkout);
        }


    }
}
