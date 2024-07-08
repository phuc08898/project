using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SGS.Application.Core.Authenticate;

namespace SGS.Infrastructure.Common.Authenticate;

public class UserIdentitfierProvider(IHttpContextAccessor httpContextAccessor) : IUserIdentifierProvider
{
    public int UserId => int.Parse(
            httpContextAccessor.HttpContext?.User.Claims
                ?.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .FirstOrDefault() ?? "-1");

    public string IpAddress => httpContextAccessor.HttpContext?.User.Claims
                ?.Where(c => c.Type == "Ip")
                .Select(c => c.Value)
                .FirstOrDefault() ?? "0.0.0.0";

    public bool IsClerical => bool.Parse(httpContextAccessor.HttpContext?.User.Claims
                ?.Where(c => c.Type == "IsClerical")
                .Select(c => c.Value)
                .FirstOrDefault() ?? "false");
}
