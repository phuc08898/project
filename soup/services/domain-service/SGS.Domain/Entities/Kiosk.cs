
using SGS.Domain.Common.Primitives;
using System.ComponentModel.DataAnnotations;
using SGS.Domain.Common.Abstractions;
using SGS.Domain.Enums;
using SGS.Domain.ValueObject;
using System.ComponentModel.DataAnnotations.Schema;


namespace SGS.Domain.Entities
{
    [Table("Kiosk")]
    public sealed class Kiosk: Entity, ISoftDeletableEntity
    {
        [StringLength(64)]
        public string Name { get; set; } = null!;
        [StringLength(128)]
        public string Address { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public DateTimeOffset? DeletedOn { get; set; }
        public string Status { get;  } = CommonStatuses.CREATED;
        public ICollection<InventorySlot>? Inventories { get; set; }
        public Kiosk() { }
        public Kiosk(string name, string address, string phonenumer)
        {
            this.Name = name;
            this.Address = address;
            this.Phonenumber = phonenumer;
        }
    }
}
