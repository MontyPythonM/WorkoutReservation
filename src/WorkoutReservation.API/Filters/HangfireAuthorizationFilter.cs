using Hangfire.Dashboard;

namespace WorkoutReservation.API.Filters;

internal class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    private readonly IServiceProvider _serviceProvider;

    public HangfireAuthorizationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public bool Authorize(DashboardContext context)
    {
        // TODO: User id from http context is null, so always throw ForbidException
        // using var scope = _serviceProvider.CreateScope();
        // var currentUserAccessor = scope.ServiceProvider.GetRequiredService<ICurrentUserAccessor>();
        //
        // try
        // {
        //     var user = currentUserAccessor.GetCurrentUserAsync(context.GetHttpContext().RequestAborted).Result;
        //     return user.IsInRole(Role.SystemAdministrator);
        // }
        // catch (Exception e)
        // {
        //     throw new ForbidException("Access forbidden. Only administrators can open HangFire dashboard.");
        // }
        return true;
    }
}
