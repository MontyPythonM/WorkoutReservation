using System.Net;
using FluentValidation;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Shared.Exceptions;
using ApplicationException = WorkoutReservation.Shared.Exceptions.ApplicationException;

namespace WorkoutReservation.API.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ConversionException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (ApplicationException ex)
        {
            context.Response.StatusCode = ex.HttpStatusCode is not null
                ? (int)ex.HttpStatusCode
                : (int)HttpStatusCode.InternalServerError;
                
            await context.Response.WriteAsync(ex.Message);
        }
        catch (InfrastructureException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Internal server error.", ex);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
