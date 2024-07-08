using System.Text.Json.Serialization;
using SGS.Domain.Enums;

namespace SGS.Application.DataTransferObjects;

public class CreateOrderArg
{
    public long Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}


public record UpdateOrderStateArg
{
    public required OrderStates State { get; init; }
    public required string OrderId { get; init; }
    public required string RequestId { get; init; }
}


// public class RabbitOrderReceiveMessage
// {
//     public int ResultCode { get; set; }
//     public string Message { get; set; } = string.Empty;
//     public required string OrderId { get; set; }
//     public required string RequestId { get; set; }
//     public required long TransId { get; set; }
// }
