using System.Text.Json.Serialization;
using FluentValidation;
namespace SGS.Domain.Common.Primitives;

public class Result
{
    public string Message { get; private set; } = "";
    public int ResultCode => IsSuccess ? 20 : ErrorCode;

    protected Result(bool isSuccess) => IsSuccess = isSuccess;
    protected Result(bool isSuccess, string msg)
    {
        IsSuccess = isSuccess;
        Message = msg;
    }
    protected Result(bool isSuccess, string msg, int errorCode)
    {
        IsSuccess = isSuccess;
        Message = msg;
        ErrorCode = errorCode;
    }

    protected Result(Exception exception, int errorCode)
    {
        IsSuccess = false;
        Message = exception.Message;
        ErrorCode = errorCode;
        Exception = exception;
        if (exception is ValidationException validationException)
        {
            Message = nameof(ValidationException);
            Errors = validationException.Errors.Select(e => e.ErrorMessage).ToArray();
        }

    }

    [JsonIgnore]
    public bool IsSuccess { get; }
    [JsonIgnore]
    public Exception? Exception { get; }
    protected int ErrorCode { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string[]? Errors { get; } = default;

    public static Result Success() => new(true);
    public static Result<TValue> Success<TValue>(TValue value, string msg) => new(value, msg, true);
    public static Result<TValue, TPage> Success<TValue, TPage>
        (TValue value, int totalPage, int totalRecords, int page, int offSet, string msg)
        => new(value, totalPage, totalRecords, page, offSet, msg, true);
    public static Result Failure(Exception ex, int errorCode) => new(ex, errorCode);
    public static Result<TResult> Failure<TResult>(Exception ex, int errorCode) => new(ex, errorCode);
    public static Result Failure(string msg, int errorCode) => new(false, msg, errorCode);
}


