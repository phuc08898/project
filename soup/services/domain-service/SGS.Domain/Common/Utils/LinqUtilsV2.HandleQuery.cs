using System.Linq.Dynamic.Core;
using SGS.Domain.Common.Primitives;
using SGS.Domain.IRepositories;

namespace SGS.Domain.Common.Utils;
public static partial class LinqUtilsV2
{
    public static IQueryable<TEn> HandleLinqQueryRequestV2<TEn, TReq>(this IRepository<TEn> repos, TReq request)
        where TEn : Entity
        where TReq : QueryRequestV2
    {

        var newQuery = request.Joins is null ?
            repos.BuildQuery :
            repos.BuildQueryWithInclude(request.Joins);

        foreach (var arg in request.Conditions ?? [])
        {
            newQuery = newQuery.HandlerLinqQueryItem(arg.Value, arg.Key);
        }

        return newQuery;
    }
    public static IQueryable<TEn> HandlerLinqQueryItem<TEn>(this IQueryable<TEn> newQuery, Dictionary<string, List<SQueryItem>> request, string condition)
    {
        foreach (var arg in request ?? [])
        {
            newQuery = arg.Key switch
            {
                "$fli" => newQuery.BuildLikeQuery(arg.Value, condition),
                "$fsw" => newQuery.BuildStartWithQuery(arg.Value, condition),
                "$few" => newQuery.BuildEndWithQuery(arg.Value, condition),
                _ => newQuery.BuildQueryWithCondition(arg.Value, condition, arg.Key switch
                {
                    "$eq" => "==",
                    "$neq" => "!=",
                    "$gt" => ">",
                    "$gte" => ">=",
                    "$lt" => "<",
                    "$lte" => "<=",
                    _ => throw new ArgumentException("Invalid condition type!")
                })
            };
        }

        return newQuery;
    }
    public static (IQueryable<TEn> query, int totalRecords, int totalPages)
        HandleLinqQueryPageRequestV2<TEn, TReq>(this IQueryable<TEn> query, TReq request)
        where TEn : Entity
        where TReq : QueryPageRequestV2
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