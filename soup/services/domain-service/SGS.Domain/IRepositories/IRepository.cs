using SGS.Domain.Common.Primitives;

namespace SGS.Domain.IRepositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetByIdAsync(string id);
   
    Task<IEnumerable<TEntity>> GetAllAsync();
    IQueryable<TEntity> BuildQuery { get; }
    IQueryable<TEntity> BuildQueryWithInclude(string[] includes);

    Task<TEntity?> FirstOrDefaultAsync(
        string id,
        IQueryable<TEntity> query,
        CancellationToken cancellationToken = default);
    Task<TEntity[]> ToArrayAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
    Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
}
