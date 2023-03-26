using FluentValidation;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetUserReservationsList;

internal sealed class GetUserReservationsListValidator : AbstractValidator<GetUserReservationsListQuery>
{
    private readonly int[] _allowedPageSizes = { 20, 50, 100 };
    private readonly string[] _allowedSortByColumnNames =
    {
        SortBySelector.ReservationStatus.StringValue(),
        SortBySelector.WorkoutDate.StringValue()
    };

    public GetUserReservationsListValidator()
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

