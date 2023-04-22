using System.Net;
using ApplicationException = WorkoutReservation.Shared.Exceptions.ApplicationException;

namespace WorkoutReservation.Application.Exceptions;

public class UserCannotDeleteOwnAccount : ApplicationException
{
    public UserCannotDeleteOwnAccount() 
        : base("You cannot delete your own account by this endpoint. Use route: ./api/account/delete-account", 
            System.Net.HttpStatusCode.Forbidden)
    {
    }
}