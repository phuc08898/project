
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Common.Primitives;
using SGS.Domain.Enums;
using SGS.Domain.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SGS.Domain.Entities
{
    [Table("Pricelist")]
    public class PriceList : Entity, IAuditableEntity,ISoftDeletableEntity
    {
        public DateTimeOffset BeginDay { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset EndDay { get; set; } = DateTimeOffset.MaxValue;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ModifiedOn { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public DateTimeOffset? DeletedOn {  get; set; }

        public string Status { get; set; } = CommonStatuses.CREATED;
        public virtual ICollection<PriceListDetail>? PriceListDetails { get; set; }
        

    }
   

    public class PriceListConfig : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {
            /*builder.HasKey(cc => new {cc.Id,cc.BeginDay, cc.EndDay });*/
        }
    }
}
