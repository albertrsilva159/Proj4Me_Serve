using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Colaboradores.Events
{
  public class ColaboradorEventHandler :
    INotificationHandler<ColaboradorRegistradoEvent>,
    INotificationHandler<ColaboradorAtualizadoEvent>,
    INotificationHandler<ColaboradorExcluidoEvent>
  {
    public Task Handle(ColaboradorExcluidoEvent message, CancellationToken cancellationTokene)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;

    }

    public Task Handle(ColaboradorAtualizadoEvent message, CancellationToken cancellationTokene)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;
    }

    public Task Handle(ColaboradorRegistradoEvent message, CancellationToken cancellationTokene)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;
    }
  }
}