using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Commands;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Domain.ProjetosAreaServicos.Commands;
using Proj4Me.Domain.ProjetosAreaServicos.Events;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleTesting
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //var bus = new FAKEbUS();

      //// Esses command quem vai lancar vai ser a camada de application
      //// registro com sucesso
      //var cmd = new RegistrarProjetoAreaServicoCommand("Projeto Simply", "projeto novo");
      //Inicio(cmd);
      //bus.SendCommand(cmd);
      //Fim(cmd);

      //// registro com erros
      //cmd = new RegistrarProjetoAreaServicoCommand("", "");
      //Inicio(cmd);
      //bus.SendCommand(cmd);
      //Fim(cmd);
      //// Atualizar Evento
      //var cmd2 = new AtualizarProjetoAreaServicoCommand(Guid.NewGuid(), "devx", "", "desclonga");
      //Inicio(cmd2);
      //bus.SendCommand(cmd2);
      //Fim(cmd2);
      //// Excluir Evento
      //var cmd3 = new ExcluirProjetoAreaServicoCommand(Guid.NewGuid());
      //Inicio(cmd3);
      //bus.SendCommand(cmd3);
      //Fim(cmd3);
      //Console.ReadKey();
    }

    //    private static void Inicio(Message message)
    //    {
    //      Console.ForegroundColor = ConsoleColor.Gray;
    //      Console.WriteLine("Iniciio comando " + message.MessageType);
    //    }

    //    private static void Fim(Message message)
    //    {
    //      Console.ForegroundColor = ConsoleColor.Gray;
    //      Console.WriteLine("Fim do comando" + message.MessageType);
    //      Console.WriteLine("");
    //      Console.ForegroundColor = ConsoleColor.Blue;
    //      Console.WriteLine("**********");
    //      Console.WriteLine("");
    //    }
    //  }
    //}
    //  public  class FAKEbUS : IBus
    //  { 
    //    public void RaiseEvent<T>(T theEvent) where T : Event
    //    {    
    //      Publish(theEvent);
    //    }

    //    public void SendCommand<T>(T theCommand) where T : Command
    //    {
    //      Console.ForegroundColor = ConsoleColor.Yellow;
    //      Console.WriteLine($"Comando { theCommand.MessageType } lançado");
    //      Publish(theCommand);
    //    }

    //    private static void Publish<T>(T message) where T : Message
    //    {
    //      var msgType = message.MessageType;

    //      if (msgType.Equals("DomainNotification"))
    //      {
    //        var obj = new DomainNotificationHandler();
    //        ((IDomainNotificationHandler<T>)obj).Handle(message);
    //      }

    //      if (msgType.Equals("RegistrarProjetoAreaServicoCommand") ||
    //          msgType.Equals("AtualizarProjetoAreaServicoCommand") || 
    //          msgType.Equals("ExcluirProjetoAreaServicoCommand"))
    //      {
    //          var obj = new ProjetoAreaServicoCommandHandler(new FakeProjetoAreaServicoRepository(), new FakeUow(), new FAKEbUS(), new DomainNotificationHandler());
    //          ((IHandler<T>)obj).Handle(message);
    //      }

    //      if (msgType.Equals("ProjetoAreaServicoRegistradoEvent") || 
    //          msgType.Equals("ProjetoAreaServicoAtualizadoEvent") || 
    //          msgType.Equals("ProjetoAreaServicoExcluidoEvent"))
    //      {
    //        var obj = new ProjetoAreaServicoEventHandler();
    //        ((IHandler<T>)obj).Handle(message);
    //    }
    //    }
    //  }

    //public class FakeProjetoAreaServicoRepository : IProjetoAreaServicoRepository
    //{
    //  public void Add(ProjetoAreaServico obj)
    //  {
    //    //
    //  }

    //  public void Dispose()
    //  {
    //    //
    //  }

    //  public IEnumerable<ProjetoAreaServico> Find(Expression<Func<ProjetoAreaServico, bool>> predicate)
    //  {
    //    throw new NotImplementedException();
    //  }

    //  public IEnumerable<ProjetoAreaServico> GetAll()
    //  {
    //    throw new NotImplementedException();
    //  }

    //  public ProjetoAreaServico GetById(Guid id)
    //  {
    //    return new ProjetoAreaServico("Fake", "fakedescricao");
    //  }

    //  public void Remover(Guid id)
    //  {
    //   //
    //  }

    //  public int SaveChanges()
    //  {
    //    throw new NotImplementedException();
    //  }

    //  public void Update(ProjetoAreaServico obj)
    //  {
    //    //
    //  }
    //}

    //public class FakeUow : IUnitOfWork
    //{
    //  public CommandResponse Commit()
    //  {
    //    return new CommandResponse(true);
    //  }

    //  public void Dispose()
    //  {
    //    throw new NotImplementedException();
    //  }
  }
}
