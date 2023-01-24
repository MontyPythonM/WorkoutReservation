using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;

public class GetWorkoutTypesListQueryValidator : AbstractValidator<GetWorkoutTypesListQuery>
{
    private readonly int[] _allowedPageSizes = new[] { 10, 20, 50 };
    private readonly string[] _allowedSortByColumnNames = { nameof(WorkoutType.Name), nameof(WorkoutType.Intensity)};

    public GetWorkoutTypesListQueryValidator()
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