using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.CreateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.DeleteRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Commands.UpdateRealWorkout;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutDetail;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;
using WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.API.Controllers;

[Route("api/real-workout/")]
public class RealWorkoutController : ApiControllerBase
{
    [HttpGet("current-week")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns a list of workouts for current week")]
    public async Task<IActionResult> GetRealWorkoutsFromCurrentWeek(CancellationToken token)
    {
        return await SendAsync(new GetRealWorkoutFromCurrentWeekQuery(), token);
    }

    [HttpGet("upcoming-week")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Returns a list of workouts for upcoming week")]
    public async Task<IActionResult> GetRealWorkoutsFromUpcomingWeek(CancellationToken token)
    {
        return await SendAsync(new GetRealWorkoutFromUpcomingWeekQuery(), token);
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

    [HttpPut]
    [HasPermission(Permission.UpdateRealWorkout)]
    [SwaggerOperation(Summary = "Update selected workout")]
    public async Task<IActionResult> UpdateRealWorkout([FromBody] UpdateRealWorkoutCommand command, CancellationToken token)
    {
        return await SendAsync(command, token);
    }
}
