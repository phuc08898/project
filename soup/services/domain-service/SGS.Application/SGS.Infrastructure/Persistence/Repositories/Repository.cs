using Microsoft.EntityFrameworkCore;
using SGS.Domain.Common.Primitives;
using SGS.Domain.IRepositories;
using SGS.Infrastructure.Persistence.RepoBuilderQueries;
using System.Linq.Dynamic.Core;

namespace SGS.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly SGSDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(SGSDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public async Task<TEntity?> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }
   
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToArrayAsync();
    }

    public IQueryable<TEntity> BuildQueryWithInclude(string[] includes)
    {
        var query = _dbSet.AsQueryable().IgnoreAutoIncludes().AsNoTracking();

        foreach (var include in includes)
        {
            var parseName = include.ToLower();
            query = parseName switch
            {
                "transactions" => query.HandleIncludeTransactionBuilder(),
                "variants" => query.HandleIncludeVariantsBuilder(),
                "categories" => query.HandleIncludeCategoryBuilder(),
                "pricelistdetail" => query.HandleIncludePriceListDetailBuilder(),
                _ => query
            };
        }

        return query.AsQueryable();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(string id, IQueryable<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<TEntity[]> ToArrayAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
    {
        return await query.ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> BuildQuery => _dbSet.AsQueryable();

}
