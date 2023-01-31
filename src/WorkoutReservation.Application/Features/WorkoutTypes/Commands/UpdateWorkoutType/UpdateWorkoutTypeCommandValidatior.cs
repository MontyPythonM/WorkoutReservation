﻿using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;

internal sealed class UpdateWorkoutTypeCommandValidatior : AbstractValidator<UpdateWorkoutTypeCommand>
{
    public UpdateWorkoutTypeCommandValidatior()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.Intensity)
            .IsInEnum()
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(600)
            .NotEmpty();
    }      
}
