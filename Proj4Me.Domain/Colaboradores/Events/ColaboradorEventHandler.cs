using Proj4Me.Domain.Core.Events;
using System;

namespace Proj4Me.Domain.Colaboradores.Events
{
  public class ColaboradorEventHandler :
    IHandler<ColaboradorRegistradoEvent>,
    IHandler<ColaboradorAtualizadoEvent>,
    IHandler<ColaboradorExcluidoEvent>
  {
    public void Handle(ColaboradorExcluidoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento excluir com sucesso!");

    }

    public void Handle(ColaboradorAtualizadoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento atualizar com sucesso!");
    }

    public void Handle(ColaboradorRegistradoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento registrar com sucesso!");
    }
  }
}
