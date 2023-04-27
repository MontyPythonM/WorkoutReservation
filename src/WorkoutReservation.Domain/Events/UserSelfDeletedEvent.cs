using WorkoutReservation.Shared.Events;

namespace WorkoutReservation.Domain.Events;

public sealed record UserSelfDeletedEvent(Guid UserId, string Email, DateTime DeletedAt) : IDomainEvent;