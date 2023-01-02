using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Presistence;
using WorkoutReservation.Infrastructure.Seeders.Data;

namespace WorkoutReservation.Infrastructure.Seeders;

public class SeedFirstAdmin
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<SeedFirstAdmin> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly FirstAdminSettings _firstAdminSettings;

    public SeedFirstAdmin(AppDbContext dbContext,
        ILogger<SeedFirstAdmin> logger,
        IPasswordHasher<User> passwordHasher,
        FirstAdminSettings firstAdminSettings)
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
        
        var anyAdmin = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserRole == UserRole.Administrator, token);
        if (anyAdmin is not null)
            return;

        var firstAdmin = FirstAdminAccount.GerFirstAdmin(_firstAdminSettings, _passwordHasher);
        await _dbContext.Users.AddAsync(firstAdmin, token);
        await _dbContext.SaveChangesAsync(token);
        _logger.LogWarning("Default administrator was seeded into database.");
    }
}
