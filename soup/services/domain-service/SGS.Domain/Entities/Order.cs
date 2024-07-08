
using System.Text.Json.Serialization;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Enums;
using SGS.Domain.Events;

namespace SGS.Domain.Entities;

public sealed class Order : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    public long Amount { get; set; }
    public string? TransId { get; set; }
    public string State { get; set; } = OrderStates.CONFIRMED;
    public string Status { get; set; } = CommonStatuses.CREATED;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedOn { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Transaction>? Transactions { get; set; }

    public Order CreateOrder()
    {
        Raise(new OrderCreatedEvent(this));
        return this;
    }
}