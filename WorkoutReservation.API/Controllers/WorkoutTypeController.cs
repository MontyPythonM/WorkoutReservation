using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.WorkoutType.Commands.CreateWorkoutType;
using WorkoutReservation.Application.Features.WorkoutType.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Application.Features.WorkoutType.Queries.GetWorkoutTypesList;

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
        public async Task<ActionResult> GetAllWorkoutTypes()
        {
            var result = await _mediator.Send(new GetWorkoutTypesListQuery());
            return Ok(result);
        }

        [HttpGet("{workoutTypeId}")]
        public async Task<ActionResult> GetWorkoutType([FromRoute] int workoutTypeId)
        {
            var result = await _mediator.Send(new GetWorkoutTypeDetailQuery() { WorkoutTypeId = workoutTypeId });
            return Ok(result);
        }

    }
}
