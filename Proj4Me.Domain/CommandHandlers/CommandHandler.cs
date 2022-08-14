using System;
using FluentValidation.Results;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;

namespace Proj4Me.Domain.CommandHandlers
{
  public  abstract class CommandHandler
  {
    private readonly IUnitOfWork _uow;
    private readonly IBus _bus;
    private readonly IDomainNotificationHandler<DomainNotification> _notificcation;

    protected CommandHandler(IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notificcation)
    {
      _uow = uow;
      _bus = bus;
      _notificcation = notificcation;
     }

    protected void NotificarValidacoesErro(ValidationResult validationResult)
    {
      foreach (var error in validationResult.Errors)
      {
        Console.WriteLine(error.ErrorMessage);
        _bus.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
      }
    }

    protected bool Commit()
    {
      // TODO: Validar se tem alguma validação de negocio com erro
      if (_notificcation.HasNotifications()) return false;
      var commandResponse = _uow.Commit();
      if (commandResponse.Success) return true;

      Console.WriteLine("Ocorreu um erro ao salvar os dados no banco");
      _bus.RaiseEvent(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));
      return false;
    }

  }
}
