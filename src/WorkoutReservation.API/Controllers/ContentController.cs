using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.Content.Commands.CreateOrUpdateHomePage;
using WorkoutReservation.Application.Features.Content.Queries.GetHomePage;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/content/")]
public class ContentController : ApiControllerBase
{
    [HttpGet("home-page")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns latest home page html content")]
    public async Task<IActionResult> GetHomePage(CancellationToken token)
    {
        return await SendAsync(new GetHomePageQuery(), token);
    }

    [HttpPost("home-page")]
    [HasPermission(Permission.CreateHomePageContent)]
    [SwaggerOperation(Summary = "Create new home page content in html format")]
    public async Task<IActionResult> CreateHomePage([FromBody] CreateOrUpdateHomePageCommand command, 
        CancellationToken token)
    {
        return await SendAsync(command, token);
    }
}