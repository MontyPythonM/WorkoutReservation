﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
