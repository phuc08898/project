namespace SGS.Application.Core.Authenticate;

/// <summary>
/// Represents the user identifier provider interface.
/// </summary>
public interface IUserIdentifierProvider
{
    public int UserId { get; }
    public string IpAddress { get; }
    public bool IsClerical { get; }
}
