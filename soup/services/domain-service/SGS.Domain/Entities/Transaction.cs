
using System.ComponentModel.DataAnnotations.Schema;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Enums;

namespace SGS.Domain.Entities;

public sealed class Transaction : Entity, IAuditableEntity
{
    [ForeignKey("Order")]
    public string OrderId { get; set; } = null!;
    public long Amount { get; set; }
    public string PaymentMethod { get; set; } = PaymentMethods.CASH;
    public string State { get; set; } = TransactionStates.PROCESSING;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedOn { get; set; }
    public Transaction() { }
    public Transaction(string orderId, long amount, string paymentMethod)
    {
        this.OrderId = orderId;
        this.Amount = amount;
        this.PaymentMethod = paymentMethod;
    }
}