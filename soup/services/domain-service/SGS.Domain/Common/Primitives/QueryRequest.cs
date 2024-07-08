
namespace SGS.Domain.Common.Primitives;

public class QueryRequest
{
    public QueryRequest()
    {
    }

    public QueryRequest(
        Dictionary<string, Dictionary<string, string>>? conditions)
    {
        Conditions = conditions;
    }
    public QueryRequest(string[]? joins)
    {
        Joins = joins;
    }

    public QueryRequest(
      Dictionary<string, Dictionary<string, string>>? conditions,
      string[]? joins)
    {
        Conditions = conditions;
        Joins = joins;
    }

    public Dictionary<string, Dictionary<string, string>>? Conditions { get; set; }
    public string[]? Joins { get; set; }
}