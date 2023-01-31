using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/reservation/")]
public class ReservationController : ApiControllerBase
{
    [HttpGet]
    [HasPermission(Permission.GetOwnReservations)]
    [SwaggerOperation(Summary = "Returns paged list of user reservations")]
    public async Task<IActionResult> GetOwnReservation([FromQuery] GetUserReservationsListQuery query, CancellationToken token)
    {
        return Ok(await Mediator.Send(query, token));
    }

    [HttpPost]
    [HasPermission(Permission.CreateReservation)]
    [SwaggerOperation(Summary = "Creates a user reservation for the selected workout")]
    public async Task<IActionResult> AddReservation([FromBody] AddReservationCommand command, CancellationToken token)
    {
        var reservationId = await Mediator.Send(command, token);

        return Created($"/api/reservation/{reservationId}", null);
    }

    [HttpPut("edit-reservation-status")]
    [HasPermission(Permission.UpdateReservationStatus)]
    [SwaggerOperation(Summary = "Change the status of a selected reservation")]
    public async Task<IActionResult> EditUserReservationStatus([FromBody] EditReservationStatusCommand command, CancellationToken token)
    {
        await Mediator.Send(command, token);
        return Ok();
    }

    [HttpPut("cancel-reservation")]
    [HasPermission(Permission.CancelReservation)]
    [SwaggerOperation(Summary = "Cancel a selected reservation")]
    public async Task<IActionResult> CancelReservation([FromBody] CancelReservationCommand command, CancellationToken token)
    {
        await Mediator.Send(command, token); 
        return Ok();
    }
}

