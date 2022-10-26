using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
    [SwaggerOperation(Summary = "Returns paged list of user reservations")]
    public async Task<IActionResult> GetReservation([FromQuery] GetUserReservationsListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation(Summary = "Creates a user reservation for the selected workout")]
    public async Task<IActionResult> AddReservation([FromBody] AddReservationCommand command)
    {
        var reservationId = await Mediator.Send(command);

        return Created($"/api/reservation/{reservationId}", null);
    }

    [HttpPut("edit-reservation-status")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Change the status of a selected reservation")]
    public async Task<IActionResult> EditUserReservationStatus([FromBody] EditReservationStatusCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut("cancel-reservation")]
    [Authorize]
    [SwaggerOperation(Summary = "Cancel a selected reservation")]
    public async Task<IActionResult> CancelReservation([FromBody] CancelReservationCommand command)
    {
        await Mediator.Send(command); 
        return Ok();
    }
}

