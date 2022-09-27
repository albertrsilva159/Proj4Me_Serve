using Proj4Me.Domain.Colaboradores.Events;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.Handlers;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using System;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Proj4Me.Domain.Colaboradores.Commands
{
  public class ColaboradorCommandHandler : CommandHandler,
      IRequestHandler<RegistrarColaboradorCommand>,
      IRequestHandler<AtualizarColaboradorCommand>,
      IRequestHandler<ExcluirColaboradorCommand>
  {

    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly IMediatorHandler _mediator;
    public ColaboradorCommandHandler(IColaboradorRepository colaboradorRepository,
                                            IUnitOfWork uow,                                  
                                            INotificationHandler<DomainNotification> notifications,
                                            IMediatorHandler mediator) : base(uow, mediator, notifications)
    {
      _colaboradorRepository = colaboradorRepository;
      _mediator = mediator;
    }

    public Task<Unit> Handle(RegistrarColaboradorCommand message, CancellationToken cancellationToken)
    {
      var colaborador = new Colaborador(message.Id, message.Nome, message.Email);

      if (!ColaboradorValido(colaborador)) return (Task<Unit>)Task.CompletedTask;

      // persistencia
      _colaboradorRepository.Add(colaborador);

      if (Commit())
      {
        //notificar um processo concluido
        Console.WriteLine("Colaborador registrado com sucesso!");
        _mediator.PublicarEvento(new ColaboradorRegistradoEvent(colaborador.Id, colaborador.Nome, colaborador.Email));
      }

      return (Task<Unit>)Task.CompletedTask;
    }

    public Task<Unit> Handle(AtualizarColaboradorCommand message, CancellationToken cancellationToken)
    {
      var colaboradorAtual = _colaboradorRepository.GetById(message.Id);
      if (!ColaboradorExistente(message.Id, message.MessageType)) return (Task<Unit>)Task.CompletedTask;

      var colaborador = new Colaborador(message.Id, message.Nome, message.Email);

      if (!ColaboradorValido(colaborador)) return (Task<Unit>)Task.CompletedTask;

      _colaboradorRepository.Update(colaborador);

      if (Commit())
      {
        _mediator.PublicarEvento(new ColaboradorAtualizadoEvent(colaborador.Id, colaborador.Nome, colaborador.Email));
      }

      return (Task<Unit>)Task.CompletedTask;
    }

    public Task<Unit> Handle(ExcluirColaboradorCommand message, CancellationToken cancellationToken)
    {
      if (!ColaboradorExistente(message.Id, message.MessageType)) return (Task<Unit>)Task.CompletedTask;

      _colaboradorRepository.Remover(message.Id);

      if (Commit())
      {
        _mediator.PublicarEvento(new ColaboradorExcluidoEvent(message.Id));
      }

      return (Task<Unit>)Task.CompletedTask;
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

      _mediator.PublicarEvento(new DomainNotification(messageType, "Evento não encontrado."));

      return false;
    } 
  }
}
