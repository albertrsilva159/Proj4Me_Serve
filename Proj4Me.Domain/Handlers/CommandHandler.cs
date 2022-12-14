using System;
using FluentValidation.Results;
using MediatR;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;


namespace Proj4Me.Domain.Handlers
{
  public abstract class CommandHandler
  {
    private readonly IUnitOfWork _uow;
    private readonly IMediatorHandler _mediator;
    private readonly DomainNotificationHandler _notifications;

    protected CommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications)
    {
      _uow = uow;
      _mediator = mediator;
      _notifications = (DomainNotificationHandler)notifications;
    }

    protected void NotificarValidacoesErro(ValidationResult validationResult)
    {
      foreach (var error in validationResult.Errors)
      {
        _mediator.PublicarEvento(new DomainNotification(error.PropertyName, error.ErrorMessage));
      }
    }

    protected bool Commit()
    {
      if (_notifications.HasNotifications()) return false;
      if (_uow.Commit()) return true;

      _mediator.PublicarEvento(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));
      return false;
    }

  }
}
