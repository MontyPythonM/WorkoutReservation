using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Users.Queries.GetUsersList
{
    public class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
    {
        private readonly int[] allowedPageSizes = new[] { 10, 25, 50, 100 };

        public readonly string[] allowedSortByColumnNames = { 
            nameof(User.Email), 
            nameof(User.FirstName),
            nameof(User.LastName),
            nameof(User.UserRole)
        };

        public GetUsersListQueryValidator()
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
}
