using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Content.Queries.GetHomePage;

public class HomePageDto
{
    public Guid Id { get; set; }
    public ContentType Type { get; set; }
    public string Value { get; set; }
}