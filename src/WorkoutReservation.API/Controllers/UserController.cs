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

    [HttpPatch("set-user-role")]
    [HasPermission(Permission.SetUserRole)]
    [SwaggerOperation(Summary = "Assigns a application role to a selected person")]
    public async Task<IActionResult> SetUserRole([FromBody] SetUserRoleCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }

    [HttpDelete("delete-user/{userGuid}")]
    [HasPermission(Permission.DeleteUserAccount)]
    [SwaggerOperation(Summary = "Delete the selected application user account")]
    public async Task<IActionResult> DeleteUserAccount([FromRoute] Guid userGuid, CancellationToken token)
    {
        return await SendAsync(new DeleteUserCommand(userGuid), token);
    }
}