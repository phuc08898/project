using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminTest.Models.Enums;

public abstract class BaseResponse
{
    public ErrorCodes ResultCode { get; set; } = ErrorCodes.Default;

    public string Message { get; set; }

    public BaseResponse(string msg)
    {
        Message = msg;
    }
    public BaseResponse(string msg, ErrorCodes errorCode)
    {
        Message = msg;
        ResultCode = errorCode;
    }
    public static OkResponse<TValue> Create<TValue>(TValue value) => new(value);

}

public class ErrorResponse(string msg) :
    BaseResponse(msg)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ErrorItem>? Errors { get; set; } = null;
}

public record ErrorItem(string Key, List<string> Messages);

public class OkResponse(string msg = "Success!") : BaseResponse(msg, ErrorCodes.NoContent)
{
}
public class MsgResponse()
{
    public string Message { get; set; }
}
public class OkResponse<TValue> :
    BaseResponse
{
    public TValue? Data { get; set; }

    public OkResponse(TValue data, string msg) : base(msg)
    {
        Data = data;
    }

    public OkResponse(TValue data) : base("Ok.")
    {
        Data = data;
    }


    public static implicit operator OkResponse<TValue>(TValue value) => Create(value);

}


public class OkPageResponse<TValue>
    (TValue data, int totalRecords, int totalPage, int page, int offSet)
    : OkResponse<TValue>(data, $"Have {totalRecords} records match."),
        IPagingResponse
{
    public int TotalRecords => totalRecords;

    public int TotalPage => totalPage;

    public int Page => page;

    public int OffSet => offSet;

    public int? NextPage => Page + 1 > TotalPage ? null : Page + 1;

    public int? PreviousPage => Page - 1 < 1 ? null : Page - 1;

}

