using System.Text.Json.Serialization;
namespace AdminTest.Models.Enums;

public class FilterCondition
{
    public string key { get; set; }
    public string val { get; set; }
}

public class Filter
{
    [JsonPropertyName("$eq")]
    public object Eq { get; set; }

    [JsonPropertyName("$gt")]
    public object Gt { get; set; }

    [JsonPropertyName("$lt")]
    public object Lt { get; set; }

    [JsonPropertyName("$gte")]
    public object Gte { get; set; }

    [JsonPropertyName("$lte")]
    public object Lte { get; set; }
}

public class Query
{
    [JsonPropertyName("$or")]
    public Filter Or { get; set; }
    [JsonPropertyName("$and")]
    public Filter And { get; set; }
}
