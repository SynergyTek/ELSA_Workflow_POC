using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class NotificationBusiness(IContextBase<NotificationViewModel, Notification> repo, IServiceProvider sp)
    : BaseBusiness<NotificationViewModel, Notification>(repo, sp), INotificationBusiness
{
    public async Task<bool> SendNotification(string userId, string message)
    {
        // Simulate sending a notification
        await Task.Delay(1000); // Simulate some delay
        return true; // Assume the notification was sent successfully
    }
}