using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Contracts;

public interface IJwtProvider
{
    string Generate(ApplicationUser user);
}