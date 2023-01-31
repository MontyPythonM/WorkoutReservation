using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Exceptions;
using WorkoutReservation.Infrastructure.Persistence;

namespace WorkoutReservation.Infrastructure.Repositories;

public class ApplicationRoleRepository : IApplicationRoleRepository
{
    private readonly AppDbContext _dbContext;
    
    public ApplicationRoleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ApplicationRole> GetAsync(Role role, CancellationToken token)
    {
        var applicationRole = await _dbContext.ApplicationRoles.FirstOrDefaultAsync(x => x.Id == (int)role, token);

        if (applicationRole is null)
            throw new InfrastructureException("Application role not exist");

        return applicationRole;
    }
}