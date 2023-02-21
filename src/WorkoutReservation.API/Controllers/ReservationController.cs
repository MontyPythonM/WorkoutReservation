using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;
using WorkoutReservation.Application.Features.Reservations.Queries.GetReservationDetails;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/reservation/")]
public class ReservationController : ApiControllerBase
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    
    public ReservationController(ICurrentUserAccessor currentUserAccessor)
    {
        _currentUserAccessor = currentUserAccessor;
    }
    
    [HttpGet("own-details")] 
    [HasPermission(Permission.GetOwnReservationDetails)]
    [SwaggerOperation(Summary = "Returns current user reservation details")]
    public async Task<IActionResult> GetOwnReservationDetails([FromQuery] GetReservationDetailsQuery query, CancellationToken token)
    {
        query.UserId = _currentUserAccessor.GetUserId();
        return Ok(await Mediator.Send(query, token));
    }
    
    [HttpGet("selected-user-details")]
    [HasPermission(Permission.GetSomeoneReservationDetails)]
    [SwaggerOperation(Summary = "Returns selected user reservation details")]
    public async Task<IActionResult> GetSomeoneReservationDetails([FromQuery] GetReservationDetailsQuery query, CancellationToken token)
    {
        return Ok(await Mediator.Send(query, token));
    }
    
    [HttpGet("own")]
    [HasPermission(Permission.GetOwnReservations)]
    [SwaggerOperation(Summary = "Returns paged list of current user reservations")]
    public async Task<IActionResult> GetOwnReservation([FromQuery] GetUserReservationsListQuery query, CancellationToken token)
    {
        query.UserId = _currentUserAccessor.GetUserId();
        return Ok(await Mediator.Send(query, token));
    }

    [HttpGet("selected-user")]
    [HasPermission(Permission.GetSomeoneReservations)]
    [SwaggerOperation(Summary = "Returns paged list of selected user reservations")]
    public async Task<IActionResult> GetSomeoneReservations([FromQuery] GetUserReservationsListQuery query, CancellationToken token)
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

    [HttpPatch("edit-reservation-status")]
    [HasPermission(Permission.UpdateReservationStatus)]
    [SwaggerOperation(Summary = "Change the status of a selected reservation")]
    public async Task<IActionResult> EditUserReservationStatus([FromBody] EditReservationStatusCommand command, CancellationToken token)
    {
        return Ok(await Mediator.Send(command, token));
    }

    [HttpPatch("cancel-reservation")]
    [HasPermission(Permission.CancelReservation)]
    [SwaggerOperation(Summary = "Cancel a selected reservation")]
    public async Task<IActionResult> CancelReservation([FromBody] CancelReservationCommand command, CancellationToken token)
    {
        return Ok(await Mediator.Send(command, token));
    }
}

