
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SGS.Domain.Common.Primitives;
using System.ComponentModel.DataAnnotations.Schema;
using SGS.Domain.Entities;
using System.Text.Json.Serialization;

namespace SGS.Domain.ValueObject;

public class InventorySlot : Entity
{
    [ForeignKey("Product")]
    public string? ProductId { get; set; } = null!;
    [JsonIgnore]
    public virtual Product? Product { get; set; }
    [ForeignKey("Kiosk")]
    public string KioskId { get; set; } = null!;
    //public Kiosk? Kiosk { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public InventorySlot()
    {

    }

}
public class ProductKioskConfig : IEntityTypeConfiguration<InventorySlot>
{
    public void Configure(EntityTypeBuilder<InventorySlot> builder)
    {
        /*builder.HasKey(cc => new { cc.ProductId, cc.KioskId });*/
    }
}
