
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Enums;
using SGS.Domain.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace SGS.Domain.Entities;

[Table("Product")]
public class Product : Entity, ISoftDeletableEntity
{
    [StringLength(64)]
    public string Name { get; set; } = null!;
    [ForeignKey("CategoryId")]
    public string CategoryId { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public int? Quantity { get; set; }
    public string? ImgUrl { get; set; }
    [JsonIgnore]
    public DateTimeOffset? DeletedOn { get; set; }
    public string Status { get; set; } = CommonStatuses.CREATED;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<Variant>? Variants { get; set; }

    public string Slug {  get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;

    public void MakeSearch()
    {
        this.Search = Name.NonUnicode();
    }
    // public ICollection<ProductKiosk>? productKiosks { get; set; }
    // public ICollection<RankProduct>? rankProducts { get; set; }
}
public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
/*        builder.HasIndex(e => e.Slug)
            .IsUnique();*/
    }
}