using FluentValidation;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Shared.TypesExtensions;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsValidator : AbstractValidator<GetReservationsQuery>
{
    private readonly int[] _allowedPageSizes = { 20, 50, 100 };
    private readonly string[] _allowedSortByColumnNames =
    {
        SortBySelector.ReservationId.StringValue(),
        SortBySelector.ReservationStatus.StringValue(),
        SortBySelector.CreatedDate.StringValue(),
        SortBySelector.LastModifiedDate.StringValue(),
        SortBySelector.WorkoutDate.StringValue(),
        SortBySelector.WorkoutName.StringValue()
    };

    public GetReservationsValidator()
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