using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class RealWorkoutAlreadyStartedException : DomainException
{
    public RealWorkoutAlreadyStartedException(int id) 
        : base($"Real workout with ID: {id} was already started")
    {
    }
}