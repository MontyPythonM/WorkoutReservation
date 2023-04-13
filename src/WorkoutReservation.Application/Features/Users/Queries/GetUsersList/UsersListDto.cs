using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Users.Queries.GetUsersList;

public class UsersListDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate  { get; set; }
    public bool IsDeleted { get; set; }
    public List<Role> Roles { get; set; }
}
