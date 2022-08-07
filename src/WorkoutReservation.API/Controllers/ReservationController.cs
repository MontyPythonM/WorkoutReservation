using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

namespace WorkoutReservation.API.Controllers;

[Route("api/reservation/")]
public class ReservationController : ApiControllerBase
{
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(PagedResultDto<UserReservationsListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReservation([FromQuery] GetUserReservationsListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddReservation([FromBody] AddReservationCommand command)
    {
        var reservationId = await Mediator.Send(command);

        return Created($"/api/reservation/{reservationId}", null);
    }

    [HttpPut("edit-reservation-status")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditUserReservationStatus([FromBody] EditReservationStatusCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut("cancel-reservation")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelReservation([FromBody] CancelReservationCommand command)
    {
        await Mediator.Send(command); 
        return Ok();
    }
}

