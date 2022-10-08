using MediatR;
using Proj4Me.Domain.Handlers;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Handlers;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Domain.Perfis.Events;
using Proj4Me.Domain.Perfis.Repository;
using System;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using Proj4Me.Domain.ProjetosAreaServicos.Events;
using System.Threading.Tasks;
using System.Threading;

namespace Proj4Me.Domain.Perfis.Commands
{
  public class PerfilCommandHandler : CommandHandler,
      IRequestHandler<RegistrarPerfilCommand>,
      IRequestHandler<AtualizarPerfilCommand>,
      IRequestHandler<ExcluirPerfilCommand>
  {

    private readonly IPerfilRepository _perfilRepository;
    private readonly IMediatorHandler _mediator;

    public PerfilCommandHandler(IPerfilRepository perfilRepository,                                           
                                            IUnitOfWork uow,
                                            IUser user,
                                            INotificationHandler<DomainNotification> notifications,
                                            IMediatorHandler mediator) : base(uow, mediator, notifications)
    {
      _perfilRepository = perfilRepository;
      _mediator = mediator;
    }
  
    public Task<Unit> Handle(RegistrarPerfilCommand message, CancellationToken cancellationToken)
    {
      var perfil = new Perfil(message.Id, message.Nome);

      if (!PerfilValido(perfil)) return (Task<Unit>)Task.CompletedTask;

      // persistencia
      _perfilRepository.Add(perfil);

      if (Commit())
      {
        //notificar um processo concluido
        Console.WriteLine("Colaborador registrado com sucesso!");
        _mediator.PublicarEvento(new PerfilRegistradoEvent(perfil.Id, perfil.Nome));
      }

      return (Task<Unit>)Task.CompletedTask;

    }

    public Task<Unit> Handle(AtualizarPerfilCommand message, CancellationToken cancellationToken)
    {
      var colaboradorAtual = _perfilRepository.GetById(message.Id);
      if (!PerfilExistente(message.Id, message.MessageType)) return (Task<Unit>)Task.CompletedTask; ;

      var perfil = new Perfil(message.Id, message.Nome);

      if (!PerfilValido(perfil)) return (Task<Unit>)Task.CompletedTask; ;

      _perfilRepository.Update(perfil);

      if (Commit())
      {
        _mediator.PublicarEvento(new PerfilAtualizadoEvent(perfil.Id, perfil.Nome));
      }
     return (Task<Unit>)Task.CompletedTask;
    }

    public Task<Unit> Handle(ExcluirPerfilCommand message, CancellationToken cancellationToken)
    {
      if (!PerfilExistente(message.Id, message.MessageType)) return (Task<Unit>)Task.CompletedTask; ;

      _perfilRepository.Remover(message.Id);

      if (Commit())
      {
        _mediator.PublicarEvento(new PerfilExcluidoEvent(message.Id));
      }
      
      return (Task<Unit>)Task.CompletedTask; ;
    }

    private bool PerfilValido(Perfil perfil)
    {

      if (perfil.EhValido()) return true;

      NotificarValidacoesErro(perfil.ValidationResult);
      return false;

    }

    private bool PerfilExistente(Guid id, string messageType)
    {
      var perfil = _perfilRepository.GetById(id);

      if (perfil != null) return true;

      _mediator.PublicarEvento(new DomainNotification(messageType, "Evento não encontrado."));
      return false;
    } 
  }
}
