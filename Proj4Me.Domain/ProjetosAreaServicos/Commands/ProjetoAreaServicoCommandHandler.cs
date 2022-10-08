using Proj4Me.Domain.Handlers;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using Proj4Me.Domain.Interfaces;
using System;
using Proj4Me.Domain.ProjetosAreaServicos.Events;
using Proj4Me.Domain.Core.Notification;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Proj4Me.Domain.ProjetosAreaServicos.Commands
{
  public class ProjetoAreaServicoCommandHandler : CommandHandler,
      IRequestHandler<RegistrarProjetoAreaServicoCommand>,
      IRequestHandler<AtualizarProjetoAreaServicoCommand>,
      IRequestHandler<ExcluirProjetoAreaServicoCommand>
  {

    private readonly IProjetoAreaServicoRepository _projetoAreaServicoRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IUser _user;
    public ProjetoAreaServicoCommandHandler(IProjetoAreaServicoRepository projetoAreaServicoRepository,
                                            IUnitOfWork uow,
                                            IUser user,
                                            INotificationHandler<DomainNotification> notifications,
                                            IMediatorHandler mediator) : base(uow, mediator, notifications)
    {
      _projetoAreaServicoRepository = projetoAreaServicoRepository;
      _user = user;
      _mediator = mediator;
    }

    public async Task<Unit> Handle(RegistrarProjetoAreaServicoCommand message, CancellationToken cancellationToken)
    {
      //ar projeto = new ProjetoAreaServico(message.Nome,message.Descricao);
      //var cliente = new Cliente(message.Cliente.Id, message.Cliente.Nome, message.Id);
      var projeto =  ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoCompleto(message.Id, message.Nome, message.Descricao, message.ColaboradorId, message.PerfilId);

      if (!ProjetoAreaServicoValido(projeto)) return  Unit.Value;  //Task.FromResult(Unit.Value);


      // persistencia
      _projetoAreaServicoRepository.Add(projeto);

      if (Commit())
      {
        //notificar um processo concluido
        Console.WriteLine("Evento registrado com sucesso!");
       await  _mediator.PublicarEvento(new ProjetoAreaServicoRegistradoEvent(projeto.Id, projeto.Nome, projeto.Descricao));
      }

      return  Unit.Value; ///Task.FromResult("ok");// (Task<Unit>)Task.CompletedTask; ///Task.FromResult(Unit.Value);

    }

    public async Task<Unit> Handle(AtualizarProjetoAreaServicoCommand message, CancellationToken cancellationToken)
    {
      var eventoAtual = _projetoAreaServicoRepository.GetById(message.Id);
      if (!EventoExistente(message.Id, message.MessageType)) return Unit.Value;// Task.FromResult(Unit.Value);

      var projeto = ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoCompleto(message.Id, message.Nome, message.Descricao, message.ColaboradorId, eventoAtual.PerfilId);

      if (!ProjetoAreaServicoValido(projeto)) return Unit.Value; //Task.FromResult(Unit.Value);

      _projetoAreaServicoRepository.Update(projeto);

      if (Commit())
      {
        await _mediator.PublicarEvento((new ProjetoAreaServicoAtualizadoEvent(projeto.Id, projeto.Nome, projeto.Descricao)));
      }

      return Unit.Value; //Task.FromResult(Unit.Value);
    }

    public async Task<Unit> Handle(ExcluirProjetoAreaServicoCommand message, CancellationToken cancellationToken)
    {
      if (!EventoExistente(message.Id, message.MessageType)) return Unit.Value;//(Task<Unit>)Task.CompletedTask; //Task.FromResult(Unit.Value);

       _projetoAreaServicoRepository.Remover(message.Id);

      if (Commit())
      {
        await _mediator.PublicarEvento((new ProjetoAreaServicoExcluidoEvent(message.Id)));
      }

      return Unit.Value; //Task.FromResult(Unit.Value);

    }

    private bool ProjetoAreaServicoValido(ProjetoAreaServico projetoAreaServico)
    {

      if (projetoAreaServico.EhValido()) return true;

      NotificarValidacoesErro(projetoAreaServico.ValidationResult);
      return false;

    }

    private bool EventoExistente(Guid id, string messageType)
    {
      var projeto = _projetoAreaServicoRepository.GetById(id);

      if (projeto != null) return true;

      _mediator.PublicarEvento((new DomainNotification(messageType, "Evento não encontrado.")));
      return false;
    }


  }
}
