using System.Text.Json;
using MediatR;
using SGS.Domain.Common.Primitives;

namespace SGS.Webapi.Types;
public class APIPageRequest : IPagingRequest, IRequest
{
    public string[]? Joins { get; set; }
    public string? S { get; set; }

    public int Limit { get; set; }

    public int Offset { get; set; }

    public int Page { get; set; }

    public static ValueTask<APIPageRequest> BindAsync(HttpContext context)
    {
        string[]? reqJoins = context.Request.Query["joins"];
        string? reqS = context.Request.Query["s"];
        _ = int.TryParse(context.Request.Query["page"], out int reqPage);
        _ = int.TryParse(context.Request.Query["limit"], out int reqLimit);
        _ = int.TryParse(context.Request.Query["offset"], out int reqOffset);

        var result = new APIPageRequest
        {
            Joins = reqJoins,
            S = reqS,
            Page = reqPage == 0 ? 1 : reqPage,
            Limit = reqLimit == 0 ? 100 : reqLimit,
            Offset = reqOffset == 0 ? 0 : reqOffset
        };

        return ValueTask.FromResult(result);
    }

    internal Dictionary<string, Dictionary<string, string>>? TryParseS()
    {
        if (this.S != null)
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(this.S);

        return null;
    }
    internal Dictionary<string, Dictionary<string, List<SQueryItem>>>? TryParseSList()
    {
        if (this.S != null)

            return JsonSerializer.Deserialize<Dictionary<string,
               Dictionary<string, List<SQueryItem>>>>(this.S);

        return null;
    }

}


public class APIRequest : IRequest
{
    public string[]? Joins { get; set; }
    public string? S { get; set; }
    public static ValueTask<APIRequest> BindAsync(HttpContext context)
    {
        string[]? reqJoins = context.Request.Query["joins"];
        string? reqS = context.Request.Query["s"];

        var result = new APIRequest
        {
            Joins = reqJoins,
            S = reqS,

        };

        return ValueTask.FromResult(result);
    }
    internal Dictionary<string, Dictionary<string, string>>? TryParseS()
    {
        if (this.S != null)
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(this.S);

        return null;
    }
    internal Dictionary<string, Dictionary<string, List<SQueryItem>>>? TryParseSList()
    {
        if (this.S != null)

            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<SQueryItem>>>>(this.S);

        return null;
    }
}