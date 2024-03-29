﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;
using WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus;
using WorkoutReservation.Application.Features.Reservations.Queries.GetReservationDetails;
using WorkoutReservation.Application.Features.Reservations.Queries.GetReservations;
using WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;
using WorkoutReservation.Domain.Enums;
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
    
    [HttpGet("details")]
    [Authorize]
    [SwaggerOperation(Summary = "Returns selected reservation details")]
    public async Task<IActionResult> GetSomeoneReservationDetails([FromQuery] GetReservationDetailsQuery query, CancellationToken token)
    {
         return await SendAsync(query, token);
    }
    
    [HttpGet("own")]
    [HasPermission(Permission.GetOwnReservations)]
    [SwaggerOperation(Summary = "Returns paged list of current user reservations")]
    public async Task<IActionResult> GetOwnReservation([FromQuery] GetUserReservationsListQuery query, CancellationToken token)
    {
        query.UserId = _currentUserAccessor.GetUserId();
        return await SendAsync(query, token);
    }

    [HttpGet("all")]
    [HasPermission(Permission.GetAllReservations)]
    [SwaggerOperation(Summary = "Returns paged list of reservations")]
    public async Task<IActionResult> GetAllReservations([FromQuery] GetReservationsQuery query, CancellationToken token)
    {
        return await SendAsync(query, token);
    }
    
    [HttpPost]
    [HasPermission(Permission.CreateReservation)]
    [SwaggerOperation(Summary = "Creates a user reservation for the selected workout")]
    public async Task<IActionResult> AddReservation([FromBody] AddReservationCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }

    [HttpPatch("edit-reservation")]
    [HasPermission(Permission.UpdateReservation)]
    [SwaggerOperation(Summary = "Change selected reservation")]
    public async Task<IActionResult> EditUserReservationStatus([FromBody] EditReservationCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }

    [HttpPatch("cancel-reservation")]
    [HasPermission(Permission.CancelReservation)]
    [SwaggerOperation(Summary = "Cancel a selected reservation")]
    public async Task<IActionResult> CancelReservation([FromBody] CancelReservationCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
}

