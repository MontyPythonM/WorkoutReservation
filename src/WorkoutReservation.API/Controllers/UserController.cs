using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Common.Dtos;
using WorkoutReservation.Application.Features.Users.Commands.DeleteUser;
using WorkoutReservation.Application.Features.Users.Commands.Login;
using WorkoutReservation.Application.Features.Users.Commands.Register;
using WorkoutReservation.Application.Features.Users.Commands.SelfUserDelete;
using WorkoutReservation.Application.Features.Users.Commands.SetUserRole;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;

namespace WorkoutReservation.API.Controllers;

[Route("api/account/")]
public class UserController : ApiControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterCommand command)
    {
        await Mediator.Send(command);
        return Ok($"Account created.");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet] 
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(typeof(PagedResultDto<UsersListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut("set-user-role")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetUserRole([FromBody] SetUserRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("delete-user/{userGuid}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUserAccount([FromRoute] Guid userGuid)
    {
        await Mediator.Send(new DeleteUserCommand { UserGuid = userGuid });
        return NoContent();
    }

    [HttpDelete("delete-account")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOwnAccount([FromBody] SelfDeleteUserCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}
