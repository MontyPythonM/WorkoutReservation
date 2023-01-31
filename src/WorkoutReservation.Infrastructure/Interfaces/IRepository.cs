using System.Linq.Expressions;

namespace WorkoutReservation.Infrastructure.Interfaces;

public interface IRepository<TEntity>
{
    public IQueryable<TEntity> ApplyAsNoTracking(bool asNoTracking, IQueryable<TEntity> query);
    public IQueryable<TEntity> ApplyIncludes(Expression<Func<TEntity, object>>[] includes, IQueryable<TEntity> query);
}