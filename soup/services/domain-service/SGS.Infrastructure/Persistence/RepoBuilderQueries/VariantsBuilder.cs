using Microsoft.EntityFrameworkCore;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;

namespace SGS.Infrastructure.Persistence.RepoBuilderQueries;

public static class VariantsBuilder
{
    public static IQueryable<TEntity> HandleIncludeVariantsBuilder<TEntity>
        (this IQueryable<TEntity> query) where TEntity : Entity
    {
        if (typeof(TEntity) == typeof(Product))
        {
            query = query.Include(e => ((Product)(object)e).Variants).AsQueryable();
        }

        return query;
    }
}