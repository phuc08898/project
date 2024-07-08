namespace SGS.Domain.ValueObject;

public class OrderItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public required string ItemName { get; set; }
    public long Price { get; set; }
    public int Quantity { get; set; }
}