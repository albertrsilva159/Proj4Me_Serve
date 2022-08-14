using Microsoft.AspNetCore.Mvc;
using Proj4Me.Domain.Core.Notification;
using System.Threading.Tasks;

namespace Proj4Me.Web.ViewComponents
{
  public class SummaryViewComponent : ViewComponent
  {
    private readonly IDomainNotificationHandler<DomainNotification> _notifications;

    public SummaryViewComponent(IDomainNotificationHandler<DomainNotification> notifications)
    {
      _notifications = notifications;
    }

    /// <summary>
    /// Todo componente precisa ter um metodo que seja async que retorna um IViewComponentResult
    /// </summary>
    /// <returns></returns>
    public async Task<IViewComponentResult> InvokeAsync()
    {
      //TASK.FROMRESULT = invoca métodos não assincronos para assincrono
      var notificacoes = await Task.FromResult(_notifications.GetNotifications());// pega as notificações 
      notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));// para cada notificacao é colocado dentro do modelstate

      return View();
    }
  }
}
