namespace SGS.Domain.Enums;

public readonly struct TransactionStates
{
    public const string PROCESSING = "PROCESSING";
    public const string SUCCEED = "SUCCEED";
    public const string REFUNED = "REFUNED";
    public const string CANCELED = "CANCELED";
}