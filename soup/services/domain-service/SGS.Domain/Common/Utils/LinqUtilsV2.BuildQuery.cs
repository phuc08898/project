using System.Linq.Dynamic.Core;
using SGS.Domain.Common.Primitives;

namespace SGS.Domain.Common.Utils;
public static partial class LinqUtilsV2
{
    public static IQueryable<TEntiy> BuildQueryWithCondition<TEntiy>
        (this IQueryable<TEntiy> currQuery, List<SQueryItem> args,
        string condition, string type)
    {
        var stringQuery = string.Empty;
        for (int i = 0; i < args.Count; i++)
        {
            stringQuery += $"{args[i].key} {type} @{i}";
            if (i < args.Count - 1)
            {
                stringQuery += condition switch
                {
                    "$or" => " || ",
                    _ => " && ",
                };
            }
        }

        currQuery = currQuery.Where(string.Format(stringQuery), args.Select(e => e.val).ToArray());
        return currQuery;
    }


    public static IQueryable<TEntiy> BuildLikeQuery<TEntiy>(this IQueryable<TEntiy> currQuery, List<SQueryItem> args, string condition)
    {

        var stringQuery = string.Empty;
        for (int i = 0; i < args.Count; i++)
        {
            stringQuery += $"{args[i].key}.Contains(@{i})";
            if (i < args.Count - 1)
            {
                stringQuery += condition switch
                {
                    "$or" => " || ",
                    _ => " && ",
                };
            }
        }

        currQuery = currQuery.Where(stringQuery, args.Select(e => e.val).ToArray());
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildStartWithQuery<TEntiy>(this IQueryable<TEntiy> currQuery, List<SQueryItem> args, string condition)
    {


        var stringQuery = string.Empty;
        for (int i = 0; i < args.Count; i++)
        {
            stringQuery += $"{args[i].key}.StartsWith(@{i})";
            if (i < args.Count - 1)
            {
                stringQuery += condition switch
                {
                    "$or" => " || ",
                    _ => " && ",
                };
            }
        }

        currQuery = currQuery.Where(stringQuery, args.Select(e => e.val).ToArray());
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildEndWithQuery<TEntiy>(this IQueryable<TEntiy> currQuery, List<SQueryItem> args, string condition)
    {
        var stringQuery = string.Empty;
        for (int i = 0; i < args.Count; i++)
        {
            stringQuery += $"{args[i].key}.EndsWith(@{i})";
            if (i < args.Count - 1)
            {
                stringQuery += condition switch
                {
                    "$or" => " || ",
                    _ => " && ",
                };
            }
        }

        currQuery = currQuery.Where(stringQuery, args.Select(e => e.val).ToArray());
        return currQuery;

    }

}