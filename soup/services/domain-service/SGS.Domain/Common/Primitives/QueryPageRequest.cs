
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SGS.Domain.Common.Primitives;

public class QueryPageRequest : QueryRequest, IPagingRequest
{
    public QueryPageRequest()
    {
    }

    public QueryPageRequest(int limit, int offset, int page)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;
    }
    public QueryPageRequest(int limit, int offset, int page,
        Dictionary<string, Dictionary<string, string>>? conditions)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;

        this.Conditions = conditions;
    }

    public QueryPageRequest(int limit, int offset, int page, string[]? joins)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;

        this.Joins = joins;
    }

    public QueryPageRequest(int limit, int offset, int page,
      Dictionary<string, Dictionary<string, string>>? conditions,
      string[]? joins)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;

        this.Conditions = conditions;
        this.Joins = joins;
    }

    public QueryPageRequest(
        Dictionary<string, Dictionary<string, string>> conditions,
        string[] joins
    )
    {
        Conditions = conditions;
        Joins = joins;
    }

    [Range(1, 1000)]
    public int Limit { get; set; } = 100;
    [Range(0, 999)]
    public int Offset { get; set; } = 0;

    public int Page { get; set; } = 1;

    [JsonIgnore]
    public int GetOffSet => Offset == 0 ?
               (Page - 1) * Limit :
               Offset;
}