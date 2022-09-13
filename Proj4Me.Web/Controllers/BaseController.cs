using Microsoft.AspNetCore.Mvc;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using System;

namespace Proj4Me.Web.Controllers
{
  public class BaseController : Controller
  {
    private readonly IDomainNotificationHandler<DomainNotification> _notifications;
    private readonly IUser _user;

    public Guid ColaboradorId { get; set; }

    public BaseController(IDomainNotificationHandler<DomainNotification> notifications,
                          IUser user)
    {
      _notifications = notifications;
      _user = user;

      if (_user.IsAuthenticated())
      {
        ColaboradorId = _user.GetUserId();
      }
    }

    protected bool OperacaoValida()
    {
      return (!_notifications.HasNotifications());
    }
  }
}
