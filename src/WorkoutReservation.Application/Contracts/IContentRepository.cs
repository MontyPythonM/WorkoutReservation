using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Contracts;

public interface IContentRepository
{
    public Task<Content> GetLastContentAsync(ContentType type, bool asNoTracking = false, CancellationToken token = default);
    public Task CreateAsync(Content content, CancellationToken token);
    public Task UpdateAsync(Content content, CancellationToken token);
}