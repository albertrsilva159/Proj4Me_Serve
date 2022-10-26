using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Clientes.Events
{
  public class ClienteEventHandler :
    INotificationHandler<ClienteRegistradoEvent>,
    INotificationHandler<ClienteAtualizadoEvent>,
    INotificationHandler<ClienteExcluidoEvent>
  {
    public Task Handle(ClienteExcluidoEvent message, CancellationToken cancellationTokene)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;

    }

    public Task Handle(ClienteAtualizadoEvent message, CancellationToken cancellationTokene)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;
    }

    public Task Handle(ClienteRegistradoEvent message, CancellationToken cancellationTokene)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;
    }
  }
}