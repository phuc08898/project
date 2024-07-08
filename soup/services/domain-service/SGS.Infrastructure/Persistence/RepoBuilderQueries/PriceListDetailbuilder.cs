using Microsoft.EntityFrameworkCore;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Infrastructure.Persistence.RepoBuilderQueries;

public static class PriceListDetailBuilder {
    public static IQueryable<TEntity> HandleIncludePriceListDetailBuilder<TEntity>
    (this IQueryable<TEntity> query) where TEntity : Entity
    {
        if (typeof(TEntity) == typeof(PriceList))
        {
            query = query.Include(e => ((PriceList)(object)e).PriceListDetails).AsQueryable();
        }

        return query;
    }
}

