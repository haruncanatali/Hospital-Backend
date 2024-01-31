using Hospital.Application.Common.Hubs;
using Hospital.Application.Common.NotificationDispatcherServices;
using Microsoft.AspNetCore.SignalR;

namespace Hospital.Application.Common.NotificationDispatchers;

public class HubStaffDispatcher : IHubStaffDispatcher
{
    private readonly IHubContext<StaffHub> _hubContext;

    public HubStaffDispatcher(IHubContext<StaffHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task CrateStaffNotification(string message)
    {
        await _hubContext.Clients.All.SendAsync("CreateStaffNotificationMessageAsync", message);
    }
}