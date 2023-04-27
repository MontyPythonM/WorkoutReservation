using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Infrastructure.Interfaces;

namespace WorkoutReservation.Infrastructure.Persistence.Interceptors;

internal sealed class AuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAuthorProvider _authorProvider;

    public AuditableEntitiesInterceptor(IDateTimeProvider dateTimeProvider, IAuthorProvider authorProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _authorProvider = authorProvider;
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        var now = _dateTimeProvider.Now;
        var author = _authorProvider.GetAuthor();
        
        foreach (var entityEntry in dbContext.ChangeTracker.Entries<Entity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(Entity.CreatedDate)).CurrentValue = now;
                entityEntry.Property(nameof(Entity.CreatedBy)).CurrentValue = author;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(Entity.LastModifiedDate)).CurrentValue = now;
                entityEntry.Property(nameof(Entity.LastModifiedBy)).CurrentValue = author;
            }
        }        
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}