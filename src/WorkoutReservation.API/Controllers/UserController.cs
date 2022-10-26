using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.Users.Commands.DeleteUser;
using WorkoutReservation.Application.Features.Users.Commands.Register;
using WorkoutReservation.Application.Features.Users.Commands.SelfUserDelete;
using WorkoutReservation.Application.Features.Users.Commands.SetUserRole;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;
using WorkoutReservation.Application.Features.Users.Queries.Login;

namespace WorkoutReservation.API.Controllers;

[Route("api/account/")]
public class UserController : ApiControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Register new user account")]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterCommand command)
    {
        await Mediator.Send(command);
        return Ok("Account created.");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Login user. If credentials is valid returns JWT Bearer token")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Returns paged list of application users")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut("set-user-role")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Assigns a application role to a selected person")]
    public async Task<IActionResult> SetUserRole([FromBody] SetUserRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("delete-user/{userGuid}")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Delete the selected application user account")]
    public async Task<IActionResult> DeleteUserAccount([FromRoute] Guid userGuid)
    {
        await Mediator.Send(new DeleteUserCommand { UserGuid = userGuid });
        return NoContent();
    }

    [HttpDelete("delete-account")]
    [Authorize]
    [SwaggerOperation(Summary = "Delete the currently logged-in user account")]
    public async Task<IActionResult> DeleteOwnAccount([FromBody] SelfDeleteUserCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}