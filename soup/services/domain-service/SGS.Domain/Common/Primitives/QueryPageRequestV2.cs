
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SGS.Domain.Common.Primitives;

public class QueryPageRequestV2 : QueryRequestV2, IPagingRequest
{
    public QueryPageRequestV2()
    {
    }

    public QueryPageRequestV2(int limit, int offset, int page)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;
    }
    public QueryPageRequestV2(int limit, int offset, int page,
        Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;

        this.Conditions = conditions;
    }

    public QueryPageRequestV2(int limit, int offset, int page, string[]? joins)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;

        this.Joins = joins;
    }

    public QueryPageRequestV2(int limit, int offset, int page,
      Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions,
      string[]? joins)
    {
        this.Limit = limit;
        this.Offset = offset;
        this.Page = page;

        this.Conditions = conditions;
        this.Joins = joins;
    }

    public QueryPageRequestV2(
        Dictionary<string, Dictionary<string, List<SQueryItem>>> conditions,
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