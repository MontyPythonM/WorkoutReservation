using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkouts;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/real-workout/")]
public class RealWorkoutController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns a list of workouts for current and upcoming weeks")]
    public async Task<IActionResult> GetRealWorkouts(CancellationToken token)
    {
        return await SendAsync(new GetRealWorkoutsQuery(), token);
    }

    [HttpGet("{realWorkoutId}")]
    [HasPermission(Permission.GetRealWorkoutDetails)]
    [SwaggerOperation(Summary = "Returns selected workout")]
    public async Task<IActionResult> GetRealWorkoutDetails([FromRoute] int realWorkoutId, CancellationToken token)
    {
        return await SendAsync(new GetRealWorkoutDetailQuery(realWorkoutId), token);
    }

    [HttpPost]
    [HasPermission(Permission.CreateRealWorkout)]
    [SwaggerOperation(Summary = "Create new workout")]
    public async Task<IActionResult> CreateRealWorkout([FromBody] CreateRealWorkoutCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }

    [HttpDelete("{realWorkoutId}")]
    [HasPermission(Permission.DeleteRealWorkout)]
    [SwaggerOperation(Summary = "Delete selected workout")]
    public async Task<IActionResult> DeleteRealWorkout([FromRoute] int realWorkoutId, CancellationToken token)
    {
        return await SendAsync(new DeleteRealWorkoutCommand(realWorkoutId), token);
    }

    [HttpPatch]
    [HasPermission(Permission.UpdateRealWorkout)]
    [SwaggerOperation(Summary = "Update selected workout")]
    public async Task<IActionResult> UpdateRealWorkout([FromBody] UpdateRealWorkoutCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
}
