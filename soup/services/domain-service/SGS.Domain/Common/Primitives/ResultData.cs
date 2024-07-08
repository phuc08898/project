using System.Text.Json.Serialization;

namespace SGS.Domain.Common.Primitives;

public class Result<TValue> : Result
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TValue? Data { get; }
    protected internal Result(TValue value, string msg, bool isSuccess)
        : base(isSuccess, msg)
    {
        Data = value;
    }

    protected internal Result(Exception exception, int errorCode)
         : base(exception, errorCode) => Data = default;

    public static implicit operator Result<TValue>(TValue value) => Success(value, "Success");
    public static implicit operator Result<TValue>((int errorCode, Exception exception) tuple)
        => (Result<TValue>)Failure(tuple.exception, tuple.errorCode);

    [JsonIgnore]
    public TValue Value => IsSuccess
        ? Data!
        // nerver throw if always check result successfully
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<Exception, TResult> failure)
        => IsSuccess ? success(Data!) : failure(Exception!);
}


public class Result<TValue, TPage> : Result, IPagingResponse
{
    public TValue Data { get; }
    protected internal Result(TValue value,
        int totalPage,
        int totalRecords,
        int page,
        int offSet,
        string msg,
        bool isSuccess)
        : base(isSuccess, msg)
    {
        Data = value;
        TotalPage = totalPage;
        TotalRecords = totalRecords;
        Page = page;
        OffSet = offSet;
    }

    public static implicit operator Result<TValue, TPage>
        ((TValue value, int totalPage, int totalRecords, int page, int offset, string msg) tuple)
        => Success<TValue, TPage>(tuple.value, tuple.totalPage, tuple.totalRecords, tuple.page, tuple.offset, tuple.msg);

    [JsonIgnore]
    public TValue Value => IsSuccess
        ? Data
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public int TotalRecords { get; private set; }
    public int TotalPage { get; private set; }
    public int Page { get; private set; }
    public int OffSet { get; private set; }
    public int? NextPage => Page + 1 > TotalPage ? null : Page + 1;
    public int? PreviousPage => Page - 1 < 1 ? null : Page - 1;

    public TResult Match<TResult>(
        Func<Result<TValue, TPage>, TResult> success,
        Func<Result<TValue, TPage>, TResult> failure)
        => IsSuccess ? success(this) : failure(this);
}