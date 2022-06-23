using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

namespace WorkoutReservation.API.Controllers
{
    [ApiController]
    [Route("/api/reservation/")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservation()
        {
            var result = await _mediator.Send(new GetUserReservationsListQuery());
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddReservation([FromBody] AddReservationCommand command)
        {
            var reservationId = await _mediator.Send(command);

            return Created($"/api/reservation/{reservationId}", null);
        }
    }
}
