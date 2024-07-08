using System.Linq.Dynamic.Core;

namespace SGS.Domain.Common.Utils;
public static partial class LinqUtils
{
    public static IQueryable<TEntiy> BuildEqualQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key} == @0", arg.Value);
        }
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildUnlikeQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key} != @0", arg.Value);
        }
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildGreaterQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key} > @0", arg.Value);
        }
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildLowerQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key} < @0", arg.Value);
        }
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildGreaterOrEqualQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key} >= @0", arg.Value);
        }
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildLowerOrEqualQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key} <= @0", arg.Value);
        }
        return currQuery;
    }

    public static IQueryable<TEntiy> BuildLikeQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key}.Contains(@0)", arg.Value);
        }
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildStartWithQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key}.StartsWith(@0)", arg.Value);
        }
        return currQuery;
    }
    public static IQueryable<TEntiy> BuildEndWithQuery<TEntiy>(this IQueryable<TEntiy> currQuery, Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            currQuery = currQuery.Where($"{arg.Key}.EndsWith(@0)", arg.Value);
        }
        return currQuery;
    }

}