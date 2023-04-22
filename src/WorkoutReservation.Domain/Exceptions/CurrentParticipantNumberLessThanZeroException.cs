using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class CurrentParticipantNumberLessThanZeroException : DomainException
{
    public CurrentParticipantNumberLessThanZeroException(int currentParticipantNumber) 
        : base($"Current participant number ({currentParticipantNumber}) cannot be less than zero")
    {
    }
}