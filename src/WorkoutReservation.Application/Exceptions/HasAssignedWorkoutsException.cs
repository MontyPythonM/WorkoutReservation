using System.Net;
using ApplicationException = WorkoutReservation.Shared.Exceptions.ApplicationException;

namespace WorkoutReservation.Application.Exceptions;

public class HasAssignedWorkoutsException : ApplicationException
{
    public HasAssignedWorkoutsException(int workoutTypeId) 
        : base($"WorkoutType with Id: {workoutTypeId} is assigned to existing workouts (repetitiveWorkout or realWorkout). " +
               $"To delete an WorkoutType, you must first delete or edit the assigned workouts.", System.Net.HttpStatusCode.Conflict)
    {
    }
}