
namespace SGS.Domain.Enums;

public readonly struct OrderStates(string currentState)
{
    public const string CONFIRMED = "CONFIRMED";
    public const string INPAYMENT = "INPAYMENT";
    public const string CANCELED = "CANCELED";
    public const string COMPLETED = "COMPLETED";
    public string Current => currentState;
}