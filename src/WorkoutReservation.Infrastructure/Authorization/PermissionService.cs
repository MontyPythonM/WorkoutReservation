using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Authorization;

public class PermissionService : IPermissionService
{
    private readonly AppDbContext _dbContext;

    public PermissionService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HashSet<string>> GetUserPermissionsAsync(Guid? userId, CancellationToken token)
    {
        var roles = await _dbContext.Set<ApplicationUser>()
            .Include(user => user.ApplicationRoles)
            .ThenInclude(role => role.ApplicationPermissions)
            .Where(user => user.Id == userId)
            .Select(user => user.ApplicationRoles)
            .ToArrayAsync(token);

        return roles
            .SelectMany(role => role)
            .SelectMany(permission => permission.ApplicationPermissions)
            .Select(permission => permission.Name)
            .ToHashSet();
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(Guid? userId, CancellationToken token)
    {
        return await _dbContext.Set<ApplicationUser>()
            .AsNoTracking()
            .Include(user => user.ApplicationRoles)
            .Where(user => user.Id == userId)
            .SelectMany(user => user.ApplicationRoles.Select(role => role.Name))
            .ToListAsync(token);
    }
}