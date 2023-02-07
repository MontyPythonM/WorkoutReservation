using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IJwtProvider
{
    Task<string> GenerateAsync(ApplicationUser user, CancellationToken token);
}