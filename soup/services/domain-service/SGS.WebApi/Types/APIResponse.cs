using SGS.Domain.Common.Primitives;
using SGS.Domain.Enums;

namespace SGS.Webapi.Types;

public sealed class APIErrorResponse(string msg, ErrorCodes errorCode) : BaseResponse(msg, errorCode)
{
}

