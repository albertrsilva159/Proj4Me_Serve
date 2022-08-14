using Proj4Me.Domain.Core.Notification;

namespace Proj4Me.Web.Controllers
{
  public class BaseController
  {
    private readonly IDomainNotificationHandler<DomainNotification> _notifications;

    public BaseController(IDomainNotificationHandler<DomainNotification> notifications)
    {
      _notifications = notifications;
    }

    protected bool OperacaoValida()
    {
      return (!_notifications.HasNotifications());
    }
  }
}
