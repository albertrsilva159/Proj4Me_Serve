using MediatR;
using Proj4Me.Domain.Core.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Perfis.Events
{
  public class PerfilEventHandler :
    INotificationHandler<PerfilRegistradoEvent>,
    INotificationHandler<PerfilAtualizadoEvent>,
    INotificationHandler<PerfilExcluidoEvent>
  {
    public Task Handle(PerfilExcluidoEvent message, CancellationToken cancellationToken)
    {
      // TODO: Disparar alguma ação
      return (Task<Unit>)Task.CompletedTask;
    }

    public Task Handle(PerfilAtualizadoEvent message, CancellationToken cancellationToken)
    {
      // TODO: Disparar alguma ação
      return (Task<Unit>)Task.CompletedTask;
    }

    public Task Handle(PerfilRegistradoEvent message, CancellationToken cancellationToken)
    {
      // TODO: Disparar alguma ação
      return (Task<Unit>)Task.CompletedTask;
    }
  }
}