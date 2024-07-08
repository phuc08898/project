using System.Linq.Dynamic.Core;
using SGS.Domain.Common.Primitives;
using SGS.Domain.IRepositories;

namespace SGS.Domain.Common.Utils;
public static partial class LinqUtils
{
    public static IQueryable<TEn> HandleLinqQueryRequest<TEn, TReq>(this IRepository<TEn> repos, TReq request)
        where TEn : Entity
        where TReq : QueryRequest
    {

        var newQuery = request.Joins is null ?
            repos.BuildQuery :
            repos.BuildQueryWithInclude(request.Joins);

        foreach (var arg in request.Conditions ?? [])
        {
            newQuery = arg.Key switch
            {
                "$eq" => newQuery.BuildEqualQuery(arg.Value),
                "$neq" => newQuery.BuildUnlikeQuery(arg.Value),

                "$gt" => newQuery.BuildGreaterQuery(arg.Value),
                "$gte" => newQuery.BuildGreaterOrEqualQuery(arg.Value),
                "$lt" => newQuery.BuildLowerQuery(arg.Value),
                "$lte" => newQuery.BuildLowerOrEqualQuery(arg.Value),

                "$fli" => newQuery.BuildLikeQuery(arg.Value),
                "$fsw" => newQuery.BuildStartWithQuery(arg.Value),
                "$few" => newQuery.BuildEndWithQuery(arg.Value),
                _ => newQuery
            };
        }

        return newQuery;
    }
    public static (IQueryable<TEn> query, int totalRecords, int totalPages)
        HandleLinqQueryPageRequest<TEn, TReq>(this IQueryable<TEn> query, TReq request)
        where TEn : Entity
        where TReq : QueryPageRequest
    {
        var totalRecords = query.Count();
        double rawTotalPages = (double)((double)totalRecords / (double)request.Limit);
        int totalPages = (int)Math.Round(rawTotalPages);
        if (rawTotalPages > totalPages)
        {
            totalPages += 1;
        }

        query = query.OrderByDescending(e => e.Id).Skip(request.GetOffSet).Take(request.Limit);

        return (query, totalRecords, totalPages);
    }
}