using System.ComponentModel.DataAnnotations;

namespace SGS.Domain.Common.Primitives;

public abstract class Entity
{
    [Key]
    public virtual string Id { get; private init; }
    protected Entity(string id) => Id = id;
    protected Entity() => Id = Guid.NewGuid().ToString("N");

}