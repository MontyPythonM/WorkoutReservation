using Hangfire.Annotations;
using Hangfire.Dashboard;
using System.Security.Claims;
using WorkoutReservation.Application.Common.Exceptions;

namespace WorkoutReservation.API.Filters;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        // TODO: check OpenHangfireDashboard permission
        var httpCtx = context.GetHttpContext();
        var userRole = httpCtx.User.FindFirst(c => c.Type == ClaimTypes.Role);

        if (userRole == null)
        {
            throw new NotFoundException("User not found. Please log in.");
        }

        if (userRole.Value == "Administrator")
        {
            return httpCtx.User.Identity.IsAuthenticated;
        }

        throw new ForbidException("Access forbidden. Only administrators can open HangFire dashboard.");
    }
}
