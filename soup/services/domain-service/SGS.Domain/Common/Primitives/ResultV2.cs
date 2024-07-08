using FluentValidation;
using SGS.Domain.Enums;

namespace SGS.Domain.Common.Primitives;

public class ResultV2
{
    public static OkResponse Success() => new("Ok.");
    public static OkResponse<TValue> Success<TValue>(TValue val) => new(val);

    public static (Exception exp, ErrorCodes code) Failure(Exception exp, ErrorCodes code)
          => new(exp, code);
    public static ResultV2<TResult> Failure<TResult>(ExceptionWithErCode exr)
            => new(exr);
    public static (Exception exp, ErrorCodes code) NotFound(Exception exp)
          => new(exp, ErrorCodes.NotFound);

    public static (Exception exp, ErrorCodes code) BadRequest(Exception exp)
          => new(exp, ErrorCodes.BadRequest);

    public static (Exception exp, ErrorCodes code) UnProcess(Exception exp)
          => new(exp, ErrorCodes.UnProcess);
}

public class ResultV2<TValue> : ResultV2
{
    private readonly TValue? _value;
    private readonly Exception? _exception;
    private readonly ErrorCodes _errorCode = ErrorCodes.Default;

    public ResultV2(TValue value)
    {
        _value = value;
        _exception = null;
        IsSuccess = true;
    }

    public ResultV2(Exception exception, ErrorCodes errorCode)
    {
        _value = default(TValue);
        _exception = exception;
        _errorCode = errorCode;
        IsSuccess = false;
    }
    public ResultV2(ExceptionWithErCode err)
    {
        _value = default(TValue);
        _exception = err.Exp;
        _errorCode = err.ErrorCode;
        IsSuccess = false;
    }

    public bool IsSuccess { get; }


    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<ExceptionWithErCode, TResult> failure
    ) => IsSuccess ?
        success(_value!) :
        failure(new ExceptionWithErCode(_exception!, _errorCode));

    public static implicit operator ResultV2<TValue>(TValue value) => new(value);
    public static implicit operator ResultV2<TValue>((Exception exp, ErrorCodes code) tuple)
        => new(tuple.exp, tuple.code);
    public static implicit operator ResultV2<TValue>(ExceptionWithErCode err)
        => new(err);

}

public class ExceptionWithErCode(Exception exp, ErrorCodes code)
{
    public Exception Exp { get; } = exp;
    public ErrorCodes ErrorCode { get; } = code;

    public ErrorResponse ToErrorResponse()
    {
        var response = new ErrorResponse(Exp.Message, ErrorCode);

        if (Exp is ValidationException vExp)
        {
            response.Message = "One or more property occured while validation.";
            // response.Errors = vExp.Errors.Select(e => e.ErrorMessage).ToArray();
            response.Errors = [];

            foreach (var er in vExp.Errors)
            {
                if (response.Errors.Any(e => e.Key.Equals(er.PropertyName)))
                {
                    response.Errors.First(e => e.Key == er.PropertyName)
                        .Messages.Add(er.ErrorMessage);
                }
                else
                {
                    response.Errors.Add(
                        new ErrorItem(er.PropertyName, [er.ErrorMessage]));
                }
            }
        }

        return response;
    }
}
