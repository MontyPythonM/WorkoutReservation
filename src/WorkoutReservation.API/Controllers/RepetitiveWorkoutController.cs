﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkoutReservation.API.Controllers.Base;
using WorkoutReservation.API.Services;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.CreateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteAllRepetitiveWorkouts;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.DeleteRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Commands.UpdateRepetitiveWorkout;
using WorkoutReservation.Application.Features.RepetitiveWorkouts.Queries.GetRepetitiveWorkoutList;

namespace WorkoutReservation.API.Controllers;

[Authorize(Roles = "Manager, Administrator")]
[Route("api/repetitive-workout/")]
public class RepetitiveWorkoutController : ApiControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Returns a list of repetitive workouts")]
    public async Task<IActionResult> GetAllRepetitiveWorkoutsPlan(CancellationToken token)
    {
        var result = await Mediator.Send(new GetRepetitiveWorkoutListQuery(), token);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create new repetitive workout")]
    public async Task<IActionResult> CreateRepetitiveWorkout([FromBody] CreateRepetitiveWorkoutCommand command, CancellationToken token)
    {
        var repetitiveWorkoutId = await Mediator.Send(command, token);
        return Created($"/api/repetitive-workout/{repetitiveWorkoutId}", null);
    }

    [HttpDelete("{repetitiveWorkoutId}")]
    [SwaggerOperation(Summary = "Delete selected repetitive workout")]
    public async Task<IActionResult> DeleteRepetitiveWorkout([FromRoute] int repetitiveWorkoutId, CancellationToken token)
    {
        await Mediator.Send(new DeleteRepetitiveWorkoutCommand() { RepetitiveWorkoutId = repetitiveWorkoutId }, token);
        return NoContent();
    }

    [HttpDelete("delete-all")]
    [SwaggerOperation(Summary = "Delete all repetitive workouts")]
    public async Task<IActionResult> DeleteAllRepetitiveWorkout(CancellationToken token)
    {
        await Mediator.Send(new DeleteAllRepetitiveWorkoutsCommand(), token);
        return NoContent();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update selected repetitive workout")]
    public async Task<IActionResult> UpdateRepetitiveWorkout([FromBody] UpdateRepetitiveWorkoutCommand command, CancellationToken token)
    {
        await Mediator.Send(command, token); 
        return Ok();
    }

    [HttpPost("generate-upcoming-week")]
    [Authorize(Roles = "Administrator")]
    [SwaggerOperation(Summary = "Forces generation of a new repetitive workouts list for the coming week")]
    public Task<IActionResult> GenerateNewUpcomingWeek(CancellationToken token)
    {
        HangfireExtension.GenerateUpcomingWeekWorkouts();
        return Task.FromResult<IActionResult>(Ok());
    }
}
