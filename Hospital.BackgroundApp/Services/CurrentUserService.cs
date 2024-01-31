using Hospital.Application.Common.Interfaces;

namespace Hospital.BackgroundApp.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
    }

    public long UserId { get; } = 1;
    public bool IsAuthenticated { get; } = false;
}