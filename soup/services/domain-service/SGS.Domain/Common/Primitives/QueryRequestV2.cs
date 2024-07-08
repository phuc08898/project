
namespace SGS.Domain.Common.Primitives;

public record SQueryItem(string key, string val);
public class QueryRequestV2
{
    public QueryRequestV2()
    {
    }

    public QueryRequestV2(
        Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions)
    {
        Conditions = conditions;
    }
    public QueryRequestV2(string[]? joins)
    {
        Joins = joins;
    }

    public QueryRequestV2(
      Dictionary<string, Dictionary<string, List<SQueryItem>>>? conditions,
      string[]? joins)
    {
        Conditions = conditions;
        Joins = joins;
    }

    public Dictionary<string, Dictionary<string, List<SQueryItem>>>? Conditions { get; set; }
    public string[]? Joins { get; set; }
}