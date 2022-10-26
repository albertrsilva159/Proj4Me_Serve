using Proj4Me.Domain.Clientes.Events;
using Proj4Me.Domain.Clientes.Repository;
using Proj4Me.Domain.Handlers;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using System;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Proj4Me.Domain.Clientes.Commands
{
  public class ClienteCommandHandler : CommandHandler,
      IRequestHandler<RegistrarClienteCommand>,
      IRequestHandler<AtualizarClienteCommand>,
      IRequestHandler<ExcluirClienteCommand>
  {

    private readonly IClienteRepository _clienteRepository;
    private readonly IMediatorHandler _mediator;
    public ClienteCommandHandler(IClienteRepository clienteRepository,
                                            IUnitOfWork uow,                                  
                                            INotificationHandler<DomainNotification> notifications,
                                            IMediatorHandler mediator) : base(uow, mediator, notifications)
    {
      _clienteRepository = clienteRepository;
      _mediator = mediator;
    }

    public Task<Unit> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
    {
      var cliente = new Cliente(message.Id, message.Nome, message.IndexClienteProj4Me);

      if (!clienteValido(cliente)) return Task.FromResult(Unit.Value);  //return (Task<Unit>)Task.CompletedTask;

      // persistencia
      _clienteRepository.Add(cliente);

      if (Commit())
      {
        //notificar um processo concluido
        Console.WriteLine("cliente registrado com sucesso!");
        _mediator.PublicarEvento(new ClienteRegistradoEvent(cliente.Id, cliente.Nome, cliente.IndexClienteProj4Me));
      }
      return Task.FromResult(Unit.Value);
      //return (Task<Unit>)Task.CompletedTask;
    }

    public Task<Unit> Handle(AtualizarClienteCommand message, CancellationToken cancellationToken)
    {
      var clienteAtual = _clienteRepository.GetById(message.Id);
      if (!clienteExistente(message.Id, message.MessageType)) return (Task<Unit>)Task.CompletedTask;

      var cliente = new Cliente(message.Id, message.Nome, message.IndexClienteProj4Me);

      if (!clienteValido(cliente)) return Task.FromResult(Unit.Value);

      _clienteRepository.Update(cliente);

      if (Commit())
      {
        _mediator.PublicarEvento(new ClienteAtualizadoEvent(cliente.Id, cliente.Nome, cliente.IndexClienteProj4Me));
      }

      return Task.FromResult(Unit.Value);
    }

    public Task<Unit> Handle(ExcluirClienteCommand message, CancellationToken cancellationToken)
    {
      if (!clienteExistente(message.Id, message.MessageType)) return Task.FromResult(Unit.Value);

      _clienteRepository.Remover(message.Id);

      if (Commit())
      {
        _mediator.PublicarEvento(new ClienteExcluidoEvent(message.Id));
      }

      return Task.FromResult(Unit.Value);
    }

    private bool clienteValido(Cliente cliente)
    {

      if (cliente.EhValido()) return true;

      NotificarValidacoesErro(cliente.ValidationResult);
      return false;

    }

    private bool clienteExistente(Guid id, string messageType)
    {
      var cliente = _clienteRepository.GetById(id);

      if (cliente != null) return true;

      _mediator.PublicarEvento(new DomainNotification(messageType, "Evento não encontrado."));

      return false;
    } 
  }
}
