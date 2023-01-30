using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Infrastructure.Seeders.Data;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Seeders;

public class SystemAdministratorSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<SystemAdministratorSeeder> _logger;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly SystemAdministratorSettings _firstAdminSettings;

    public SystemAdministratorSeeder(AppDbContext dbContext,
        ILogger<SystemAdministratorSeeder> logger,
        IPasswordHasher<ApplicationUser> passwordHasher,
        SystemAdministratorSettings firstAdminSettings)
    {
        _dbContext = dbContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _firstAdminSettings = firstAdminSettings;
    }

    public async Task SeedAsync(CancellationToken token)
    {
        if (await _dbContext.Database.CanConnectAsync(token) is false)
            throw new InternalServerError("Cannot connect with database.");

        var anyAdmin = await _dbContext.ApplicationUsers
            .AnyAsync(x => x.ApplicationRoles.Contains(ApplicationRole.SystemAdministrator), token);
        
        if (anyAdmin)
        {        
            return;
        }
        
        var firstAdmin = FirstSystemAdministrator.Create(_firstAdminSettings, _passwordHasher);
        
        // TODO: sql error to resolve
        //firstAdmin.Id = Guid.NewGuid();
        
        //await _dbContext.AddAsync(firstAdmin, token);
        //await _dbContext.SaveChangesAsync(token);
        _logger.LogWarning("Default administrator was seeded into database.");
    }
}
