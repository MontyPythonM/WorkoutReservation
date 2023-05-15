using WorkoutReservation.Shared.Events;

namespace WorkoutReservation.Domain.Events;

public sealed record ReservationCancelledEvent(int ReservationId) : IDomainEvent;