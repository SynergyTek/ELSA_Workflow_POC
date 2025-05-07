using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface;

public interface INotificationBusiness : IBaseBusiness<NotificationViewModel, Notification>
{
    Task<bool> SendNotification(string userId, string message);
}