using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservation.Application.Features.Users.Commands.Login;
using WorkoutReservation.Application.Features.Users.Commands.Register;

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
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok($"Account created.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }
    }
}
