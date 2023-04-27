using WorkoutReservation.Shared.Events;

namespace WorkoutReservation.Domain.Events;

public sealed record UserDeletedByAdministratorEvent(Guid UserId, string Email, DateTime DeletedAt) : IDomainEvent;