using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SGS.Domain.ValueObject;

public class PriceListDetail : Entity
{
    [ForeignKey("Variant")]
    public string VariantId { get; set; } = null!;
    [ForeignKey("PriceList")]
    public string PriceListId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public int DiscountPercent { get; set; }
    public long Pricre { get; set; }
    [JsonIgnore]
    public virtual Variant Variant { get; set; } = null!;
    [JsonIgnore]
    public virtual PriceList PriceList { get; set; } = null!;
    public PriceListDetail() { }
    public PriceListDetail(string variantId, string productId, string priceListId, int discountPc, long price)
    {
        VariantId = variantId;
        ProductId = productId;
        PriceListId = priceListId;
        DiscountPercent = discountPc;
        Pricre = price;
    }
}
public class PriceListDetailConfig : IEntityTypeConfiguration<PriceListDetail>
{
    public void Configure(EntityTypeBuilder<PriceListDetail> builder)
    {
        /*        builder
                    .HasOne(ptv => ptv.PriceList)
                    .WithMany(pt => pt.PriceListDetails)
                    .HasForeignKey(ptv => new { ptv.PriceListId, ptv.BeginDay, ptv.EndDay });
        */
    }
}
