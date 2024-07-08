using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Common.Utils;
using SGS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SGS.Domain.Entities;

public sealed class Category : Entity,ISoftDeletableEntity
{
    [ForeignKey("Category")]
    public string? ParentId { get; set; }
    [JsonIgnore]
    public Category? Parent { get; set; }
    public string Name { get; set;} = string.Empty;

    public DateTimeOffset? DeletedOn {  get; set; }

    public string Status { get; set; } = CommonStatuses.CREATED;
    public string Slug { get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;

    public void MakeSearch()
    {
        this.Search = Name.NonUnicode();
    }
    public Category() { }
    public Category(string name,string parentId)
    { 
        this.ParentId = parentId;
        this.Name = name;
    }
    public Category(string name)
    {
        this.ParentId = null;
        this.Name = name;
    }
}

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
/*        builder.HasIndex(e => e.Slug)
            .IsUnique();*/
    }
}
