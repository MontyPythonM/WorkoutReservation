using Microsoft.AspNetCore.Identity;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Interfaces;

public interface IFirstSystemAdministrator
{
    public Task<ApplicationUser> Create(SystemAdministratorSettings settings,
        IPasswordHasher<ApplicationUser> passwordHasher, CancellationToken token);
}