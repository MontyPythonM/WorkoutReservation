using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IJwtProvider
{
    public Task<string> GenerateAsync(ApplicationUser user, CancellationToken token);
}