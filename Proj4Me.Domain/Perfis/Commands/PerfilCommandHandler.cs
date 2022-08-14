using Proj4Me.Domain.CommandHandlers;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Domain.Perfis.Events;
using Proj4Me.Domain.Perfis.Repository;
using System;

namespace Proj4Me.Domain.Perfis.Commands
{
  public class PerfilCommandHandler : CommandHandler,
      IHandler<RegistrarPerfilCommand>,
      IHandler<AtualizarPerfilCommand>,
      IHandler<ExcluirPerfilCommand>
  {

    private readonly IPerfilRepository _perfilRepository;
    private readonly IBus _bus;
    public PerfilCommandHandler(IPerfilRepository perfilRepository
                                           , IUnitOfWork uow
                                           , IBus bus
                                           , IDomainNotificationHandler<DomainNotification> notificcation) : base(uow, bus, notificcation)
    {
      _perfilRepository = perfilRepository;
      _bus = bus;
    }

    public void Handle(RegistrarPerfilCommand message)
    {
      var perfil = new Perfil(message.Id, message.Nome);

      if (!PerfilValido(perfil)) return;

      // persistencia
      _perfilRepository.Add(perfil);

      if (Commit())
      {
        //notificar um processo concluido
        Console.WriteLine("Colaborador registrado com sucesso!");
        _bus.RaiseEvent(new PerfilRegistradoEvent(perfil.Id, perfil.Nome));
      }

    }

    public void Handle(AtualizarPerfilCommand message)
    {
      var colaboradorAtual = _perfilRepository.GetById(message.Id);
      if (!PerfilExistente(message.Id, message.MessageType)) return;

      var perfil = new Perfil(message.Id, message.Nome);

      if (!PerfilValido(perfil)) return;

      _perfilRepository.Update(perfil);

      if (Commit())
      {
        _bus.RaiseEvent(new PerfilAtualizadoEvent(perfil.Id, perfil.Nome));
      }
    }

    public void Handle(ExcluirPerfilCommand message)
    {
      if (!PerfilExistente(message.Id, message.MessageType)) return;

      _perfilRepository.Remover(message.Id);

      if (Commit())
      {
        _bus.RaiseEvent(new PerfilExcluidoEvent(message.Id));
      }

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

      _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado."));
      return false;
    } 
  }
}
