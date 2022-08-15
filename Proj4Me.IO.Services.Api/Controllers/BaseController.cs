using Microsoft.AspNetCore.Mvc;
using Proj4Me.Domain.Core.Notification;
using System.Collections.Generic;
using System.Linq;

namespace Proj4Me.IServices.Api.Controllers
{
  [Produces("*Application/Json")]
  public abstract class BaseController : Controller
  {
    private readonly IDomainNotificationHandler<DomainNotification> _notifications;

    protected BaseController(IDomainNotificationHandler<DomainNotification> notifications)
    {
      _notifications = notifications;
    }

    protected new IActionResult Response(object result = null)
    {
      if (OperacaoValida())
      {
        return Ok(new
        {
          success = true,
          data = result
        });
      }

      return BadRequest(new
      {
        success = false,
        errors = _notifications.GetNotifications().Select(n => n.Value)
      });
    }

    protected bool OperacaoValida()
    {
      return (!_notifications.HasNotifications());
    }


  }
}
