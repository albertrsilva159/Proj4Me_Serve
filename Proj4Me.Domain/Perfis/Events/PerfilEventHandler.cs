using Proj4Me.Domain.Core.Events;
using System;


namespace Proj4Me.Domain.Perfis.Events
{
  public class PerfilEventHandler :
    IHandler<PerfilRegistradoEvent>,
    IHandler<PerfilAtualizadoEvent>,
    IHandler<PerfilExcluidoEvent>
  {
    public void Handle(PerfilExcluidoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento excluir com sucesso!");

    }

    public void Handle(PerfilAtualizadoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento atualizar com sucesso!");
    }

    public void Handle(PerfilRegistradoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento registrar com sucesso!");
    }
  }
}
