using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.Users.Commands.DeleteUser;
using WorkoutReservation.Application.Features.Users.Commands.SetUserRole;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/user/")]
public class UserController : ApiControllerBase
{
    [HttpGet("users")]
    [HasPermission(Permission.GetAllUsers)]
    [SwaggerOperation(Summary = "Returns paged list of application users")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersListQuery query, CancellationToken token)
    {
        return await SendAsync(query, token);
    }

    [HttpPatch("set-user-roles")]
    [HasPermission(Permission.SetUserRole)]
    [SwaggerOperation(Summary = "Assigns a application roles to a selected user")]
    public async Task<IActionResult> SetUserRoles([FromBody] SetUserRoleCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }

    [HttpDelete("delete-user")]
    [HasPermission(Permission.DeleteUserAccount)]
    [SwaggerOperation(Summary = "Delete the selected application user account")]
    public async Task<IActionResult> DeleteUserAccount([FromBody] DeleteUserCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
}