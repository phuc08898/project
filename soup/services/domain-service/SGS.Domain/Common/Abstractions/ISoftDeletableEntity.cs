namespace SGS.Domain.Common.Abstractions;

/// <summary>
/// Represents the marker interface for soft-deletable entities.
/// </summary>
public interface ISoftDeletableEntity
{
    DateTimeOffset? DeletedOn { get; }
    string Status { get; }
}