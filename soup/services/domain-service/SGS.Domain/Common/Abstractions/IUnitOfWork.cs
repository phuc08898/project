using SGS.Domain.Common.Primitives;
using SGS.Domain.IRepositories;

namespace SGS.Infrastructure.Common.Data;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangeAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    bool IsInTransaction { get; }
    bool IsSaved { get; }
    void SetIsSave(bool isSave);
}