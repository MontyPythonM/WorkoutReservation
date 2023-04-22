using System.Net;

namespace WorkoutReservation.Shared.Exceptions;

public class ApplicationException : Exception
{
    public HttpStatusCode? HttpStatusCode { get; private set; } = default;
    
    public ApplicationException(string message) : base(message)
    {
    }
    
    public ApplicationException(string message, HttpStatusCode httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}