using Proj4Me.Domain.Colaboradores.Events;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.CommandHandlers;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using System;

namespace Proj4Me.Domain.Colaboradores.Commands
{
  public class ColaboradorCommandHandler : CommandHandler,
      IHandler<RegistrarColaboradorCommand>,
      IHandler<AtualizarColaboradorCommand>,
      IHandler<ExcluirColaboradorCommand>
  {

    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly IBus _bus;
    public ColaboradorCommandHandler(IColaboradorRepository colaboradorRepository
                                           , IUnitOfWork uow
                                           , IBus bus
                                           , IDomainNotificationHandler<DomainNotification> notificcation) : base(uow, bus, notificcation)
    {
      _colaboradorRepository = colaboradorRepository;
      _bus = bus;
    }

    public void Handle(RegistrarColaboradorCommand message)
    {
      var colaborador = new Colaborador(message.Id, message.Nome, message.Email);

      if (!ColaboradorValido(colaborador)) return;

      // persistencia
      _colaboradorRepository.Add(colaborador);

      if (Commit())
      {
        //notificar um processo concluido
        Console.WriteLine("Colaborador registrado com sucesso!");
        _bus.RaiseEvent(new ColaboradorRegistradoEvent(colaborador.Id, colaborador.Nome, colaborador.Email));
      }

    }

    public void Handle(AtualizarColaboradorCommand message)
    {
      var colaboradorAtual = _colaboradorRepository.GetById(message.Id);
      if (!ColaboradorExistente(message.Id, message.MessageType)) return;

      var colaborador = new Colaborador(message.Id, message.Nome, message.Email);

      if (!ColaboradorValido(colaborador)) return;

      _colaboradorRepository.Update(colaborador);

      if (Commit())
      {
        _bus.RaiseEvent(new ColaboradorAtualizadoEvent(colaborador.Id, colaborador.Nome, colaborador.Email));
      }
    }

    public void Handle(ExcluirColaboradorCommand message)
    {
      if (!ColaboradorExistente(message.Id, message.MessageType)) return;

      _colaboradorRepository.Remover(message.Id);

      if (Commit())
      {
        _bus.RaiseEvent(new ColaboradorExcluidoEvent(message.Id));
      }

    }

    private bool ColaboradorValido(Colaborador colaborador)
    {

      if (colaborador.EhValido()) return true;

      NotificarValidacoesErro(colaborador.ValidationResult);
      return false;

    }

    private bool ColaboradorExistente(Guid id, string messageType)
    {
      var colaborador = _colaboradorRepository.GetById(id);

      if (colaborador != null) return true;

      _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado."));
      return false;
    } 
  }
}
