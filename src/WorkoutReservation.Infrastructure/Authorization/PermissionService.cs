using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Authorization;

public class PermissionService : IPermissionService
{
    private readonly AppDbContext _dbContext;

    public PermissionService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(Guid? userId)
    {
        var roles = await _dbContext.Set<ApplicationUser>()
            .Include(user => user.ApplicationRoles)
            .ThenInclude(role => role.ApplicationPermissions)
            .Where(user => user.Id == userId)
            .Select(user => user.ApplicationRoles)
            .ToArrayAsync();

        return roles
            .SelectMany(role => role)
            .SelectMany(permission => permission.ApplicationPermissions)
            .Select(permission => permission.Name)
            .ToHashSet();
    }
}