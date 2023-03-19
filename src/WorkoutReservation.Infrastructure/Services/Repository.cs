using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Infrastructure.Interfaces;

namespace WorkoutReservation.Infrastructure.Services;

internal sealed class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : class
{
    public IQueryable<TEntity> ApplyAsNoTracking(bool asNoTracking, IQueryable<TEntity> query) => 
        asNoTracking ? query.AsNoTracking() : query;
    
    public IQueryable<TEntity> ApplyIncludes(Expression<Func<TEntity, object>>[] includes, IQueryable<TEntity> query) => 
        includes.Any() ? includes.Aggregate(query, (current, include) => current.Include(include)) : query;
}