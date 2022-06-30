using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.Users.Commands.DeleteUser;
using WorkoutReservation.Application.Features.Users.Commands.Login;
using WorkoutReservation.Application.Features.Users.Commands.Register;
using WorkoutReservation.Application.Features.Users.Commands.SelfUserDelete;
using WorkoutReservation.Application.Features.Users.Commands.SetUserRole;
using WorkoutReservation.Application.Features.Users.Queries.GetUsersList;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.API.Controllers
{
    [ApiController]
    [Route("/api/account/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok($"Account created.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetUsersListQuery());
            return Ok(result);
        }

        [HttpPut("set-user-role")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetUserRole([FromBody] SetUserRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("delete-user/{userGuid}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUserAccount([FromRoute] Guid userGuid)
        {
            await _mediator.Send(new DeleteUserCommand { UserGuid = userGuid });
            return NoContent();
        }

        [HttpDelete("delete-account")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOwnAccount([FromBody] SelfDeleteUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
