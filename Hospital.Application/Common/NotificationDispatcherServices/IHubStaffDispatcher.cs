namespace Hospital.Application.Common.NotificationDispatcherServices;

public interface IHubStaffDispatcher
{
    Task CrateStaffNotification(string message);
}