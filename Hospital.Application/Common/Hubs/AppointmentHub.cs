using Microsoft.AspNetCore.SignalR;

namespace Hospital.Application.Common.Hubs;

public sealed class AppointmentHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("dbEventMessage", message);
    }
}