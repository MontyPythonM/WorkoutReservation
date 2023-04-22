using System.Net;
using ApplicationException = WorkoutReservation.Shared.Exceptions.ApplicationException;

namespace WorkoutReservation.Application.Exceptions;

public class UserCannotSeeOtherUsersReservationException : ApplicationException
{
    public UserCannotSeeOtherUsersReservationException() 
        : base("User cannot see other users reservations", System.Net.HttpStatusCode.Forbidden)
    {
    }
}