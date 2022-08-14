using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.CommandHandlers;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using Proj4Me.Domain.Interfaces;
using System;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.ProjetosAreaServicos.Events;
using Proj4Me.Domain.Core.Notification;

namespace Proj4Me.Domain.ProjetosAreaServicos.Commands
{
  public class ProjetoAreaServicoCommandHandler : CommandHandler,
      IHandler<RegistrarProjetoAreaServicoCommand>,
      IHandler<AtualizarProjetoAreaServicoCommand>,
      IHandler<ExcluirProjetoAreaServicoCommand>
  {

    private readonly IProjetoAreaServicoRepository _projetoAreaServicoRepository;
    private readonly IBus _bus;
    public ProjetoAreaServicoCommandHandler(IProjetoAreaServicoRepository projetoAreaServicoRepository
                                           , IUnitOfWork uow
                                           , IBus bus
                                           , IDomainNotificationHandler<DomainNotification> notificcation) : base(uow, bus, notificcation)
    {
      _projetoAreaServicoRepository = projetoAreaServicoRepository;
      _bus = bus;
    }

    public void Handle(RegistrarProjetoAreaServicoCommand message)
    {
      //ar projeto = new ProjetoAreaServico(message.Nome,message.Descricao);
      //var cliente = new Cliente(message.Cliente.Id, message.Cliente.Nome, message.Id);
      var projeto = ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoCompleto(message.Id, message.Nome, message.Descricao, message.ColaboradorId, message.PerfilId);

      if (!ProjetoAreaServicoValido(projeto)) return;


      // persistencia
      _projetoAreaServicoRepository.Add(projeto);

      if (Commit())
      {
        //notificar um processo concluido
        Console.WriteLine("Evento registrado com sucesso!");
        _bus.RaiseEvent(new ProjetoAreaServicoRegistradoEvent(projeto.Id, projeto.Nome, projeto.Descricao));
      }

    }

    public void Handle(AtualizarProjetoAreaServicoCommand message)
    {
      var eventoAtual = _projetoAreaServicoRepository.GetById(message.Id);
      if (!EventoExistente(message.Id, message.MessageType)) return;

      var projeto = ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoCompleto(message.Id, message.Nome, message.Descricao, message.ColaboradorId, eventoAtual.PerfilId);

      if (!ProjetoAreaServicoValido(projeto)) return;

      _projetoAreaServicoRepository.Update(projeto);

      if (Commit())
      {
        _bus.RaiseEvent(new ProjetoAreaServicoAtualizadoEvent(projeto.Id, projeto.Nome, projeto.Descricao));
      }
    }

    public void Handle(ExcluirProjetoAreaServicoCommand message)
    {
      if (!EventoExistente(message.Id, message.MessageType)) return;

      _projetoAreaServicoRepository.Remover(message.Id);

      if (Commit())
      {
        _bus.RaiseEvent(new ProjetoAreaServicoExcluidoEvent(message.Id));
      }

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

      _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado."));
      return false;
    }

  }
}
