using System;
using System.Linq;
using Proj4Me.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Proj4Me.Domain.Core.Notification;
using Microsoft.AspNetCore.Cors;

namespace Proj4Me.Services.Api.Controllers
{
  
  [Produces("application/json")]
  public abstract class BaseController : Controller
  {
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediator;
    public Guid ColaboradorId { get; set; }

    protected BaseController(INotificationHandler<DomainNotification> notifications,
                                 IUser user,
                                 IMediatorHandler mediator)
    {
      _notifications = (DomainNotificationHandler)notifications;
      //_mediator = mediator;
      _mediator = mediator;
      if (user.IsAuthenticated())
      {
        ColaboradorId = user.GetUserId();
      }
      
    }
    //o new é para dizer que esse response não é nativo, vou usar o proprio response
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

    protected void NotificarErroModelInvalida()
    {
      var erros = ModelState.Values.SelectMany(v => v.Errors);
      foreach (var erro in erros)
      {
        var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
        NotificarErro(string.Empty, erroMsg);
      }
    }

    protected void NotificarErro(string codigo, string mensagem)
    {
      _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
    }

    protected void AdicionarErrosIdentity(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        NotificarErro(result.ToString(), error.Description);
      }
    }
  }
}