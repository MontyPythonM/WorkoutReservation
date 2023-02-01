﻿using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Users.Queries.GetUsersList;

internal sealed class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
{
    private readonly int[] _allowedPageSizes = new[] { 15, 30, 50 };
    private readonly string[] _allowedSortByColumnNames = { 
        nameof(ApplicationUser.Email), 
        nameof(ApplicationUser.FirstName),
        nameof(ApplicationUser.LastName),
    };

    public GetUsersListQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!_allowedPageSizes.Contains(value))
            {
                context.AddFailure("PageSize", $"PageSize must in [{string.Join(", ", _allowedPageSizes)}]");
            }
        });

        RuleFor(x => x.SortBy).Custom((value, context) =>
        {
            if (value is not null && !_allowedSortByColumnNames.Contains(value))
            {
                context.AddFailure("SortBy", $"SortBy must in [{string.Join(", ", _allowedSortByColumnNames)}]");
            }
        });
    }
}

