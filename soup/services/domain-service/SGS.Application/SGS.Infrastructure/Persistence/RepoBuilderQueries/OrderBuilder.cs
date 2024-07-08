using Microsoft.EntityFrameworkCore;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;

namespace SGS.Infrastructure.Persistence.RepoBuilderQueries;

public static class OrderBuilder
{
    public static IQueryable<TEntity> HandleIncludeTransactionBuilder<TEntity>(this IQueryable<TEntity> query) where TEntity : Entity
    {
        if (typeof(TEntity) == typeof(Order))
            query = query.Include(e => ((Order)(object)e).Transactions).AsQueryable();

        return query;
    }
}