using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Exceptions;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Seeders;

public class SystemAdministratorSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<SystemAdministratorSeeder> _logger;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly SystemAdministratorSettings _firstAdminSettings;
    private readonly IFirstSystemAdministrator _firstSystemAdministrator;
    
    public SystemAdministratorSeeder(AppDbContext dbContext, ILogger<SystemAdministratorSeeder> logger,
        IPasswordHasher<ApplicationUser> passwordHasher, SystemAdministratorSettings firstAdminSettings, 
        IFirstSystemAdministrator firstSystemAdministrator)
    {
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _firstAdminSettings = firstAdminSettings;
        _firstSystemAdministrator = firstSystemAdministrator;
    }

    public async Task SeedAsync(CancellationToken token = default)
    {
        if (await _dbContext.Database.CanConnectAsync(token) is false)
            throw new CannotConnectToDatabaseException();

        var anyAdmin = await _dbContext.ApplicationUsers
            .AnyAsync(x => x.ApplicationRoles.Contains(ApplicationRole.SystemAdministrator), token);
        
        if (anyAdmin)
        {        
            return;
        }
        
        var firstAdmin = await _firstSystemAdministrator.Create(_firstAdminSettings, _passwordHasher, token);
        
        await _dbContext.AddAsync(firstAdmin, token);
        await _dbContext.SaveChangesAsync(token);
        _logger.LogWarning("Default administrator was seeded into database.");
    }
}
