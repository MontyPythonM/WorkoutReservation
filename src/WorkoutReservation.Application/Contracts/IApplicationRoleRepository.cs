using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Contracts;

public interface IApplicationRoleRepository
{
    public Task<ApplicationRole> GetAsync(Role role, CancellationToken token);
}