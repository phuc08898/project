using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace AdminTest.Models.Enums;

public class JsonBuilder
{
    public static string BuildJson(Query? request, string[]? joins, int? limit, int? page)
    {       
        if (request == null && limit == null && limit <= 0 && page != null && page <= 0 && joins.IsNullOrEmpty())
            return null;
        var respone = "?";
        if (joins != null)
        {
            foreach(var join in joins)
                respone +=  "joins=" +join + "&";
            respone = respone.Substring(0, respone.Length - 1);
        }
        if(request!= null)
        {
            if (respone == "?")
            {
            }
                var jsonOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var json = JsonSerializer.Serialize(request, jsonOptions);
                respone += "s=" + json;
                       
        }
        if (limit != null && limit > 0)
        {
            if (respone == "?")
            {
                respone += "&";
            }
            if (page != null && page > 0)
            {
                if (respone == "?")
                {
                    respone += "&";
                }
                respone += "page=" + page;
            }
            respone += "limit=" + limit;
            
        }   
        return respone;
    }
}
