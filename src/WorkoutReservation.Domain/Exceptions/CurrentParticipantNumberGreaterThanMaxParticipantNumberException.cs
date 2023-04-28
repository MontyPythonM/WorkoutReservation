using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class CurrentParticipantNumberGreaterThanMaxParticipantNumberException : DomainException
{
    public CurrentParticipantNumberGreaterThanMaxParticipantNumberException(int currentParticipantNumber, int maxParticipantNumber) 
        : base($"Current participant number ({currentParticipantNumber}) cannot be greater than maximum participant number ({maxParticipantNumber})")
    {
    }
}