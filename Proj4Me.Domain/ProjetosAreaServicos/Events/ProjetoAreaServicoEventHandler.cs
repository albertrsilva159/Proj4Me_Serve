using MediatR;
using Proj4Me.Domain.Core.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Proj4Me.Domain.ProjetosAreaServicos.Events
{
  /// <summary>
  /// a ideia é fazer um tipo log da informacao, a ideia é gravar o estada da entidade saber quando foi criado, excluido ou alterado
  /// </summary>
  public class ProjetoAreaServicoEventHandler :
    INotificationHandler<ProjetoAreaServicoRegistradoEvent>,
    INotificationHandler<ProjetoAreaServicoAtualizadoEvent>,
    INotificationHandler<ProjetoAreaServicoExcluidoEvent>
  {
    public Task Handle(ProjetoAreaServicoExcluidoEvent messag, CancellationToken cancellationTokene)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;
    }

    public Task Handle(ProjetoAreaServicoAtualizadoEvent message, CancellationToken cancellationToken)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;
    }

    public Task Handle(ProjetoAreaServicoRegistradoEvent message, CancellationToken cancellationToken)
    {
      // TODO: Disparar alguma ação
      return Task.CompletedTask;
    }
  }
}
