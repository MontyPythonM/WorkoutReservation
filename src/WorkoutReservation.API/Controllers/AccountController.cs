using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.Account.Commands.Register;
using WorkoutReservation.Application.Features.Account.Commands.SelfUserDelete;
using WorkoutReservation.Application.Features.Account.Queries.GetCurrentUser;
using WorkoutReservation.Application.Features.Account.Queries.Login;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/account/")]
public class AccountController : ApiControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Register new user account")]
    public async Task<IActionResult> RegisterAccount([FromBody] RegisterCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Login user. If credentials is valid returns JWT Bearer token")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken token)
    {
        return await SendAsync(query, token);
    }
    
    [HttpDelete("delete-account")]
    [HasPermission(Permission.DeleteOwnAccount)]
    [SwaggerOperation(Summary = "Delete the currently logged-in user account")]
    public async Task<IActionResult> DeleteOwnAccount([FromBody] SelfDeleteUserCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
    
    [HttpGet("current-user")]
    [Authorize]
    [SwaggerOperation(Summary = "Get information about current user")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken token)
    {
        return await SendAsync(new GetCurrentUserQuery(), token);
    }
}