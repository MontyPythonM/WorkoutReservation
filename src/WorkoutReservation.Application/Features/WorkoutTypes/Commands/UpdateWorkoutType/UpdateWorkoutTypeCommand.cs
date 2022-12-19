﻿using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;

public class UpdateWorkoutTypeCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkoutIntensity Intensity { get; set; }
    public List<int> WorkoutTypeTags { get; set; }
    public List<int> Instructors { get; set; }
}
