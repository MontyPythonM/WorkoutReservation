using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;

public class GetWorkoutTypesListQueryValidator : AbstractValidator<GetWorkoutTypesListQuery>
{
    private readonly int[] allowedPageSizes = new[] { 5, 10, 15 };
    public readonly string[] allowedSortByColumnNames = { nameof(WorkoutType.Name), nameof(WorkoutType.Intensity)};

    public GetWorkoutTypesListQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
            {
                context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
            }
        });

        RuleFor(x => x.SortBy).Custom((value, context) =>
        {
            if (value is not null)
            {
                foreach (var SortByColumnName in allowedSortByColumnNames)
                {
                    if (!allowedSortByColumnNames.Contains(value))
                    {
                        context.AddFailure("SortBy", $"SortBy must in [{string.Join(", ", allowedSortByColumnNames)}]");
                        break;
                    }
                }
            }
        });
    }
}