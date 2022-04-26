using RiverSong.Shared.Application.Contracts;

namespace RiverSong.Customers.Api.Services;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> GetCurrentUserName()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return Task.FromResult(httpContext?.User?.Identity?.Name ?? "unknown user");
    }
}