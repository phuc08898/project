using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SGS.Domain.Entities;

public class Variant : Entity,ISoftDeletableEntity
{
    [ForeignKey("Product")]
    [JsonIgnore]
    public string ProductId { get; set; } = null!;
/*    [JsonIgnore]
    public virtual Product? Product { get; set; } = null;*/
    public string Name { get; set; } = string.Empty;
    public long Price { get; set; }
    public DateTimeOffset? DeletedOn {  get; set; }

    public string Status { get; set; } = CommonStatuses.CREATED;

    public Variant() { }
    public Variant(string productId, string name, long price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }
}
