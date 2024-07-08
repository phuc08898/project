
namespace SGS.Domain.Enums;

public readonly struct PaymentMethods
{
    public const string MOMO = "MOMO";
    public const string VNPAY = "VNPAY";
    public const string CASH = "CASH";
    public const string ACCEPTER = "ACCEPTER";

    public static bool IsEnum(string pt)
    {
        return pt switch
        {
            MOMO => true,
            VNPAY => true,
            CASH => true,
            ACCEPTER => true,
            _ => false
        };
    }

}