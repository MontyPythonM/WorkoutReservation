using FluentValidation;

namespace WorkoutReservation.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsValidator : AbstractValidator<GetReservationsQuery>
{
    private readonly int[] _allowedPageSizes = { 20, 50, 100 };
    
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
    }
}