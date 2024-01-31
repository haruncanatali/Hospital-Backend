using Microsoft.AspNetCore.SignalR;

namespace Hospital.Application.Common.Hubs;

public class StaffHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("GetConnectionId", Context.ConnectionId);
        Console.WriteLine("User Connected. USERID: " + Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine("User Disconnected! USERID: " + Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task CreateStaffNotificationMessageAsync(string message)
    {
        await Clients.All.SendAsync("CreateStaffNotificationMessageAsync", message);
    }

    public async Task SendMessage(string content)
    {
        await Clients.All.SendAsync("ReceiveMessage", content);
    }
}